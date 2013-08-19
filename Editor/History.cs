﻿// Copyright (C) Wiesław Šoltés 2013. 
// All Rights Reserved

#region References

using CanvasDiagramEditor.Core;
using CanvasDiagramEditor.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace CanvasDiagramEditor.Editor
{
    #region Aliases

    using MapPin = Tuple<string, string>;
    using MapWire = Tuple<object, object, object>;
    using MapWires = Tuple<object, List<Tuple<string, string>>>;
    using Selection = Tuple<bool, List<Tuple<object, object, object>>>;
    using UndoRedo = Tuple<Stack<string>, Stack<string>>;
    using Diagram = Tuple<string, Tuple<Stack<string>, Stack<string>>>;
    using TreeDiagram = Stack<string>;
    using TreeDiagrams = Stack<Stack<string>>;
    using TreeProject = Tuple<string, Stack<Stack<string>>>;
    using TreeProjects = Stack<Tuple<string, Stack<Stack<string>>>>;
    using TreeSolution = Tuple<string, string, Stack<Tuple<string, Stack<Stack<string>>>>>;
    using Position = Tuple<double, double>;
    using Connection = Tuple<IElement, List<Tuple<object, object, object>>>;
    using Connections = List<Tuple<IElement, List<Tuple<object, object, object>>>>;
    using Solution = Tuple<string, IEnumerable<string>>;

    #endregion

    #region History

    public static class History
    {
        #region History

        public static UndoRedo Get(ICanvas canvas)
        {
            var canvasTag = canvas.GetTag();
            if (canvasTag == null)
            {
                canvasTag = new UndoRedo(new Stack<string>(), new Stack<string>());
                canvas.SetTag(canvasTag);
            }

            var tuple = canvasTag as UndoRedo;

            return tuple;
        }

        public static string Add(ICanvas canvas)
        {
            var tuple = Get(canvas);
            var undoHistory = tuple.Item1;
            var redoHistory = tuple.Item2;

            var model = Model.GenerateDiagram(canvas, null, canvas.GetProperties());

            undoHistory.Push(model);

            redoHistory.Clear();

            return model;
        }

        public static void RollbackUndo(ICanvas canvas)
        {
            var tuple = Get(canvas);
            var undoHistory = tuple.Item1;
            var redoHistory = tuple.Item2;

            if (undoHistory.Count <= 0)
                return;

            // remove unused history
            undoHistory.Pop();
        }

        public static void RollbackRedo(ICanvas canvas)
        {
            var tuple = Get(canvas);
            var undoHistory = tuple.Item1;
            var redoHistory = tuple.Item2;

            if (redoHistory.Count <= 0)
                return;

            // remove unused history
            redoHistory.Pop();
        }

        public static void Clear(ICanvas canvas)
        {
            var tuple = Get(canvas);
            var undoHistory = tuple.Item1;
            var redoHistory = tuple.Item2;

            undoHistory.Clear();
            redoHistory.Clear();
        }

        public static void Undo(ICanvas canvas, IDiagramCreator creator, bool pushRedo)
        {
            var tuple = Get(canvas);
            var undoHistory = tuple.Item1;
            var redoHistory = tuple.Item2;

            if (undoHistory.Count <= 0)
                return;

            // save current model
            if (pushRedo == true)
            {
                var current = Model.GenerateDiagram(canvas, null, canvas.GetProperties());
                redoHistory.Push(current);
            }

            // resotore previous model
            var model = undoHistory.Pop();

            Model.Clear(canvas);
            Model.Parse(model,
                canvas, creator,
                0, 0,
                false, true, false, true);
        }

        public static void Redo(ICanvas canvas, IDiagramCreator creator, bool pushUndo)
        {
            var tuple = Get(canvas);
            var undoHistory = tuple.Item1;
            var redoHistory = tuple.Item2;

            if (redoHistory.Count <= 0)
                return;

            // save current model
            if (pushUndo == true)
            {
                var current = Model.GenerateDiagram(canvas, null, canvas.GetProperties());
                undoHistory.Push(current);
            }

            // resotore previous model
            var model = redoHistory.Pop();

            Model.Clear(canvas);
            Model.Parse(model,
                canvas, creator,
                0, 0,
                false, true, false, true);
        } 
        
        #endregion
    }

    #endregion
}
