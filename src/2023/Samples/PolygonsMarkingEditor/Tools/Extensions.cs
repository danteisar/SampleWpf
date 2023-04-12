﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace PolygonsMarkingEditor.Tools;

internal static class Extensions
{
    public static T FindVisualParent<T>(this DependencyObject depObj) where T : DependencyObject
    {
        while (true)
        {
            var parentObject = VisualTreeHelper.GetParent(depObj);

            switch (parentObject)
            {
                case null:
                    return null;
                case T parent:
                    return parent;
                default:
                    depObj = parentObject;
                    continue;
            }
        }
    }

    public static T GetChildOfType<T>(this DependencyObject depObj)
        where T : DependencyObject
    {
        if (depObj == null) return null;

        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
            var child = VisualTreeHelper.GetChild(depObj, i);

            var result = (child as T) ?? GetChildOfType<T>(child);
            if (result != null) return result;
        }
        return null;
    }

    public static IEnumerable<T> FindVisualChildren<T>([NotNull] this DependencyObject parent) where T : DependencyObject
    {
        if (parent == null)
            throw new ArgumentNullException(nameof(parent));

        var queue = new Queue<DependencyObject>(new[] { parent });

        while (queue.Any())
        {
            var reference = queue.Dequeue();
            var count = VisualTreeHelper.GetChildrenCount(reference);

            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(reference, i);
                if (child is T children)
                    yield return children;

                queue.Enqueue(child);
            }
        }
    }

    public const int ElementSize = 5;
}

