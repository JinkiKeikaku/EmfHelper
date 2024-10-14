namespace EmfHelper
{
    public interface IEmfPlusReadListener
    {
        void OnEnterEmfPlusRecords();
        void OnExitEmfPlusRecords();

        void OnEmfPlusHeader(EmfPlusRecord record, EmfPlusHeader header);
        void OnEmfPlusGetDC(EmfPlusRecord record);
        void OnEmfPlusEndOfFile(EmfPlusRecord record);

        void OnEmfPlusNotImplementRecord(EmfPlusRecord record);

        void OnEmfPlusObject(EmfPlusRecord record, EmfPlusObject obj);

        void OnEmfPlusClear(EmfPlusRecord record, int color);

        void OnEmfPlusDrawLines(EmfPlusRecord record, IReadOnlyList<PointF> points, bool isClosed, int objectId);
        void OnEmfPlusDrawLines(EmfPlusRecord record, byte[] data, int numPoints, bool isClosed, int objectId);

        void OnEmfPlusFillPolygon(EmfPlusRecord record, IReadOnlyList<PointF> points, int brushId, bool isBrushIdColor);
        void OnEmfPlusFillPolygon(EmfPlusRecord record, byte[] data, int numPoints, int brushId, bool isBrushIdColor);

        void OnEmfPlusDrawRects(EmfPlusRecord record, IReadOnlyList<EmfPlusRectF> rects, int objectId);
        void OnEmfPlusFillRects(EmfPlusRecord record, IReadOnlyList<EmfPlusRectF> rects, int brushId, bool isBrushIdColor);

        void OnEmfPlusDrawEllipse(EmfPlusRecord record, EmfPlusRectF rect, int objectId);
        void OnEmfPlusFillEllipse(EmfPlusRecord record, EmfPlusRectF rect, int brushId, bool isBrushIdColor);

        void OnEmfPlusDrawArc(EmfPlusRecord record, EmfPlusRectF rect, Single startAngle, Single sweepAngle, int objectId);

        void OnEmfPlusDrawPie(EmfPlusRecord record, EmfPlusRectF rect, Single startAngle, Single sweepAngle, int objectId);
        void OnEmfPlusFillPie(EmfPlusRecord record, EmfPlusRectF rect, Single startAngle, Single sweepAngle, int brushId, bool isBrushIdColor);

        void OnEmfPlusDrawPath(EmfPlusRecord record, int objectId, int penId);
        void OnEmfPlusFillPath(EmfPlusRecord record, int objectId, int brushId, bool isBrushIdColor);

        void OnEmfPlusDrawBeziers(EmfPlusRecord record, IReadOnlyList<PointF> points, int objectId);
        void OnEmfPlusDrawBeziers(EmfPlusRecord record, byte[] data, int numPoints, int objectId);

        void OnEmfPlusDrawCurve(EmfPlusRecord record, IReadOnlyList<PointF> points, int offset, int numSegments, float tension,int objectId);
        void OnEmfPlusDrawClosedCurve(EmfPlusRecord record, IReadOnlyList<PointF> points, float tension, int objectId);
        void OnEmfPlusDrawClosedCurve(EmfPlusRecord record, byte[] data, int numPoints, float tension, int objectId);
        void OnEmfPlusFillClosedCurve(EmfPlusRecord record, IReadOnlyList<PointF> points, float tension, bool isWindingFill, int brushId, bool isBrushIdColor);
        void OnEmfPlusFillClosedCurve(EmfPlusRecord record, byte[] data, int numPoints, float tension, bool isWindingFill, int brushId, bool isBrushIdColor);

        void OnEmfPlusDrawString(EmfPlusRecord record, int objectId, int formatID, EmfPlusRectF layoutRect, char[] stringData, int brushId, bool isBrushIdColor);
        
        void OnEmfPlusDrawImage(EmfPlusRecord record, EmfPlusRectF rectData, int objectId, int imageAttributesID, EmfPlusUnitType srcUnit, EmfPlusRectF srcRect);
        void OnEmfPlusDrawImagePoints(EmfPlusRecord record, IReadOnlyList<PointF> points, int objectId, int imageAttributesID, EmfPlusUnitType srcUnit, EmfPlusRectF srcRect);
        void OnEmfPlusDrawImagePoints(EmfPlusRecord record, byte[] data, int numPoints, int objectId, int imageAttributesID, EmfPlusUnitType srcUnit, EmfPlusRectF srcRect);

        void OnEmfPlusResetWorldTransform(EmfPlusRecord record);
        void OnEmfPlusSetWorldTransform(EmfPlusRecord record, Single[] matrix);
        void OnEmfPlusMultiplyWorldTransform(EmfPlusRecord record, Single[] matrix, bool isPostMultiplied);
        void OnEmfPlusScaleWorldTransform(EmfPlusRecord record, Single sx, Single sy, bool isPostMultiplied);
        void OnEmfPlusRotateWorldTransform(EmfPlusRecord record, Single angle, bool isPostMultiplied);
        void OnEmfPlusTranslateWorldTransform(EmfPlusRecord record, Single dx, Single dy, bool isPostMultiplied);
        void OnEmfPlusSave(EmfPlusRecord record, int stackIndex);
        void OnEmfPlusRestore(EmfPlusRecord record, int stackIndex);
        void OnEmfPlusSetAntiAliasMode(EmfPlusRecord record, EmfPlusSmoothingMode mode, bool isAntiAliasing);

    }
}
