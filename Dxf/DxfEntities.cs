﻿// Copyright (C) Wiesław Šoltés 2013. 
// All Rights Reserved

#region References

using CanvasDiagramEditor.Dxf.Entities;
using CanvasDiagramEditor.Dxf.Enums;
using CanvasDiagramEditor.Dxf.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace CanvasDiagramEditor.Dxf
{
    #region DxfEntities

    public static class DxfEntities
    {
        #region DxfEntitiesBegin

        public static string DxfEntitiesBegin()
        {
            var b = new DxfBuilder();

            // begin entities section
            b.Add("0", "SECTION");
            b.Add("2", "ENTITIES");

            return b.Build();
        } 

        #endregion

        #region DxfEntitiesEnd

        public static string DxfEntitiesEnd()
        {
            var b = new DxfBuilder();

            // end entities section
            b.Add("0", "ENDSEC");

            return b.Build();
        } 

        #endregion

        #region DxfLwpolyline
 
        public static string DxfLwpolyline(int numberOfVertices,
            DxfLwpolylineFlags lwpolylineFlags,
            double constantWidth,
            double elevation,
            double thickness,
            DxfLwpolylineVertex [] vertices,
            Vector3 extrusionDirection,
            string layer,
            string color)
        {
            var b = new DxfBuilder();

            // lwpolyline
            b.Add("0", "LWPOLYLINE");

            // layer
            if (layer != null)
            {
                b.Add("8", layer);
            }

            // color
            if (color != null)
            {
                b.Add("62", color);
            }

            // number of vertices
            b.Add("90", numberOfVertices);

            // polyline flags
            if (lwpolylineFlags != DxfLwpolylineFlags.Default)
            {
                b.Add("70", (int)lwpolylineFlags);
            }

            // constant width
            if (constantWidth != 0.0)
            {
                b.Add("43", constantWidth);
            }

            // elevation 
            if (elevation != 0.0)
            {
                b.Add("38", elevation);
            }

            // thickness 
            if (thickness != 0.0)
            {
                b.Add("39", thickness);
            }

            if (vertices != null)
            {
                // vertices
                foreach(var vertex in vertices)
                {
                    // vertex coordinates: X
                    b.Add("10", vertex.Coordinates.X);

                    // vertex coordinates: Y
                    b.Add("20", vertex.Coordinates.Y);

                    if (constantWidth == 0.0)
                    {
                        // starting width
                        if (vertex.StartWidth != 0.0)
                        {
                        b.Add("40", vertex.StartWidth);
                        }

                        // end width
                        if (vertex.EndWidth != 0.0)
                        {
                            b.Add("41", vertex.EndWidth);
                        }
                    }

                    // bulge
                    if (vertex.Bulge != 0.0)
                    {
                        b.Add("42", vertex.Bulge);
                    }
                }
            }

            if (extrusionDirection != null)
            {
                // extrusion direction: X
                b.Add("210", extrusionDirection.X);

                // extrusion direction: Y
                b.Add("220", extrusionDirection.Y);

                // extrusion direction: Z
                b.Add("230", extrusionDirection.Z);
            }

            return b.Build();
        }

        #endregion
    }

    #endregion
}