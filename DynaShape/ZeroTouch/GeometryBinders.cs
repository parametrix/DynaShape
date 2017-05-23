﻿using System.Collections.Generic;
using DSCore;
using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Runtime;
using DynaShape.GeometryBinders;
using Point = Autodesk.DesignScript.Geometry.Point;


namespace DynaShape.ZeroTouch
{
    public static class GeometryBinders
    {
        public static LineBinder LineBinder(
            Line line,
            [DefaultArgument("null")] Color color)
        {
            return new LineBinder(
                line.StartPoint.ToTriple(), 
                line.EndPoint.ToTriple(), 
                color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor);
        }


        public static LineBinder LineBinder(
            Point startPoint,
            Point endPoint,
            [DefaultArgument("null")] Color color)
        {
            return new LineBinder(
                startPoint.ToTriple(), 
                endPoint.ToTriple(),
                color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor);
        }


        public static PolylineBinder PolylineBinder(
            List<Point> vertices,
            [DefaultArgument("null")] Color color)
        {
            return new PolylineBinder(
                vertices.ToTriples(),
                color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor);
        }


        public static MeshBinder MeshBinder(
            Mesh mesh,
            [DefaultArgument("null")] Color color)
        {
            return new MeshBinder(
                mesh,
                color?.ToColor4() ?? DynaShapeDisplay.DefaultLineColor);
        }


        public static GeometryBinder ChangeColor(GeometryBinder geometryBinder, Color color)
        {
            geometryBinder.Color = color.ToColor4();
            return geometryBinder;
        }
    }
}