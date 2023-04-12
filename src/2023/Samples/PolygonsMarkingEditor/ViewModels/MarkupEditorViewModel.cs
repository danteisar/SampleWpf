using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DynamicData;
using DynamicData.Binding;
using Microsoft.Win32;
using PolygonsMarkingEditor.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolygonsMarkingEditor.ViewModels;

internal class MarkupEditorViewModel : ReactiveObject
{
    public IList<Polygon> Polygons { get; } = new List<Polygon>();
    public ObservableCollection<ReactiveObject> Elements { get; } = new();

    [Reactive] public Vertex Vertex { get; set; } = new();
    [Reactive] public Image Image { get; set; } = new();

    public MarkupEditorViewModel()
    {   
        Add = ReactiveCommand.Create(OnAdd);
        End = ReactiveCommand.Create(OnEnd);

        var cObs = Elements
            .ToObservableChangeSet()
            .ToCollection()
            .Select(_ => Elements)
            .Select(x => x.Count > 0);

        Clear = ReactiveCommand.Create(OnClear, cObs);
        Cancel = ReactiveCommand.Create(OnCancel, cObs);
        ClearSelectedItemsCommand = ReactiveCommand.Create(OnClearSelected);
        AddImage = ReactiveCommand.Create(OnAddImage);
    }


    public ICommand Add { get; }
    public ICommand End { get; }
    public ICommand Clear { get; }  
    public ICommand Cancel { get; }
    public ICommand ClearSelectedItemsCommand { get; }
    public ICommand AddImage { get; }

    private void OnAdd()
    {
        if (Polygons.All(x => x.IsEnded))
            Polygons.Add(new Polygon());

        var polygon = Polygons.First(x => !x.IsEnded);

        var point = new Vertex(Vertex);

        if (polygon.Points.Count > 0)
            Elements.Insert(0, new VertexLine(polygon.Points.Last(), point));

        polygon.Points.Add(point);
        Elements.Add(point);
    }

    private void OnEnd()
    {
        var polygon = Polygons.FirstOrDefault(x => !x.IsEnded);
        if (polygon == null) return;

        polygon.IsEnded = true;
        Elements.Insert(0, new VertexLine(polygon.Points.Last(), polygon.Points.First()));
    }

    private void OnClear()
    {
        Polygons.Clear();
        Elements.Clear();
    }   
        
    private void Update()
    {
        Elements.Clear();
        foreach (var polygon in Polygons)
        {
            var start = polygon.Points.FirstOrDefault();
            if (start == null) continue;

            for (var i = 1; i < polygon.Points.Count; i++)
            {
                Elements.Add(new VertexLine(start, polygon.Points[i]));
                start = polygon.Points[i];
            }

            if (polygon.IsEnded)
            {
                Elements.Add(new VertexLine(polygon.Points.Last(), polygon.Points.First()));
            }

            foreach (var point in polygon.Points)
            {
                Elements.Add(point);
            }
        }
    }

    private void OnCancel()
    {
        if (!Polygons.Any()) return;

        var polygon = Polygons.Last();

        if (polygon.Points.Count == 0)
        {
            Polygons.Remove(polygon);

            OnCancel();
        }

        if (polygon.IsEnded)
        {
            polygon.IsEnded = false;
        }
        else
        {
            if (polygon.Points.Any())
            {
                var t = polygon.Points.Last();
                if (t != null) polygon.Points.Remove(t);
            }
        }

        Update();
    }

    private void OnClearSelected()
    {
        foreach (var polygon in Polygons)
        {
            foreach (var polygonPoint in polygon.Points)
            {
                polygonPoint.IsSelected = false;
            }
        }
    }

    private void OnAddImage()
    {
        var dl1 = new OpenFileDialog
        {
            FileName = "*",
            DefaultExt = ".png",
            Filter = "Image documents (.png)|*.png"
        };
        var result = dl1.ShowDialog();
        if (result != true) return;
        var filename = dl1.FileName;
        var image = new BitmapImage(new Uri(@filename, UriKind.Relative));
        var brush = new ImageBrush
        {
            ImageSource = image
        };

        OnClear();

        Image.Brush = brush;
        Image.Width = image.Width;
        Image.Height = image.Height;
    }

}       