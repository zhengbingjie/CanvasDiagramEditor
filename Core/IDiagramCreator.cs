﻿// Copyright (C) Wiesław Šoltés 2013. 
// All Rights Reserved

#region References

using CanvasDiagramEditor.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

#endregion

namespace CanvasDiagramEditor.Core
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

    #region IDiagramCreator

    public interface IDiagramCreator
    {
        void SetCanvas(ICanvas canvas);
        ICanvas GetCanvas();

        object CreatePin(double x, double y, int id, bool snap);

        object CreateWire(double x1, double y1, double x2, double y2,
            bool startVisible, bool endVisible,
            bool startIsIO, bool endIsIO,
            int id);

        object CreateInput(double x, double y, int id, int tagId, bool snap);
        object CreateOutput(double x, double y, int id, int tagId, bool snap);

        object CreateAndGate(double x, double y, int id, bool snap);
        object CreateOrGate(double x, double y, int id, bool snap);
        object CreateDiagram(DiagramProperties properties);

        object CreateGrid(double originX, double originY, double width, double height, double size);

        void UpdateConnections(IDictionary<string, MapWires> dict);
        void UpdateCounter(IdCounter original, IdCounter counter);

        void AppendIds(IEnumerable<object> elements);

        void InsertElements(IEnumerable<object> elements, bool select);
    } 

    #endregion
}
