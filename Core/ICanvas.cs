﻿// Copyright (C) Wiesław Šoltés 2013. 
// All Rights Reserved

#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace CanvasDiagramEditor.Core
{
    #region ICanvas

    public interface ICanvas : IData, IUid, ITag, ISelected
    {
        IEnumerable<IElement> GetElements();

        void Add(IElement element);
        void Remove(IElement element);
        void Clear();

        IEnumerable<IElement> HitTest(IPoint point, double radius);
        IEnumerable<IElement> HitTest(IRect rect);
    }

    #endregion
}
