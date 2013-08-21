﻿// Copyright (C) Wiesław Šoltés 2013. 
// All Rights Reserved

#region References

using CanvasDiagram.Dxf.Core;
using CanvasDiagram.Dxf.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace CanvasDiagram.Dxf.Entities
{
    #region Dxf3Dface

    public class Dxf3Dface : DxfObject<Dxf3Dface>
    {
        public Dxf3Dface(DxfAcadVer version, int id)
            : base(version, id)
        {
        }
    }

    #endregion
}