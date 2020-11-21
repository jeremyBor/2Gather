using Base.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace TTBreakOut
{
    [RequireComponent(typeof(CanvasScaler))]
    public class ScreenInformation : MonoBehaviourSingleton<ScreenInformation>
    {
        private readonly Vector2 _referenceResolution = new Vector2(720f, 1440f);
        private const float Match = 0f;

        private Vector2 _baseReferenceResolution;
        
        private CanvasScaler _cs;

        private Vector2 _canvasShiftValue;
        
        public static float Ratio => (float) Screen.width / Screen.height;

        private ScreenOrientation _screenOrientation;
        
        public float CanvasRealWidth
        {
            get
            {
                float matchWidth = _cs.referenceResolution.x * (1 - _cs.matchWidthOrHeight);
                float matchHeight = (float) Screen.width / Screen.height * _cs.referenceResolution.y *
                                    _cs.matchWidthOrHeight;

                return matchWidth + matchHeight;
            }
        }

        public float CanvasRealHeight
        {
            get
            {
                float matchWidth = _cs.referenceResolution.y * _cs.matchWidthOrHeight;
                float matchHeight = (float) Screen.height / Screen.width * _cs.referenceResolution.x *
                                    (1 - _cs.matchWidthOrHeight);

                return matchWidth + matchHeight;
            }
        }

        protected override void SingletonAwake()
        {
            _cs = GetComponent<CanvasScaler>();
            //Check.IsTrue(_cs != null);

            _baseReferenceResolution = new Vector2(
                _referenceResolution.x,
                _referenceResolution.y * Mathf.Lerp(
                    1.25f,
                    1f,
                    Mathf.InverseLerp((4f / 3f), (16f / 9f), Ratio)));
            
            _cs.matchWidthOrHeight = Match;
            _cs.referenceResolution = _baseReferenceResolution;
            
            // Init
            _screenOrientation = GetScreenOrientation();
            UpdateSafeArea();
        }

        private void Update()
        {
            ScreenOrientation newScreenOrientation = GetScreenOrientation();
            if (_screenOrientation != newScreenOrientation)
            {
                _screenOrientation = newScreenOrientation;
                UpdateSafeArea();
            }
        }

        public Vector2 WorldToScreenPoint(Vector3 worldPosition)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPosition);

            screenPoint.x *= CanvasRealWidth / Screen.width;
            screenPoint.y *= CanvasRealHeight / Screen.height;
            
            screenPoint.x -= CanvasRealWidth * _canvasShiftValue.x;
            screenPoint.y -= CanvasRealHeight * _canvasShiftValue.y;
            
            return screenPoint;
        }

        public float WorldToUIUnit(float worldUnit, bool useReferenceResolution = false)
        {
            return worldUnit * 1 / _cs.transform.localScale.x * Camera.main.orthographicSize *
                   (useReferenceResolution ? (_referenceResolution.y / _cs.referenceResolution.y) : 1f);
        }

        /// <summary>
        /// Transforms position from ui space into world space.
        /// UI space is defined in pixels. The bottom-left of the screen is (0,0); the right-top is (CanvasRealWidth, CanvasRealHeight).
        /// </summary>
        public Vector3 UiToWorld3DUnit(Vector2 uiPosition, float distanceToCamera)
        {
            return Camera.main.ScreenToWorldPoint(
                new Vector3(
                    uiPosition.x * Screen.width / CanvasRealWidth, 
                    uiPosition.y * Screen.height / CanvasRealHeight,
                    distanceToCamera));
        }

        public Vector3 UiToWorld3DUnit(Camera c, Vector2 uiPosition, float distanceToCamera)
        {
            return c.ScreenToWorldPoint(
                new Vector3(
                    uiPosition.x * Screen.width / CanvasRealWidth,
                    uiPosition.y * Screen.height / CanvasRealHeight,
                    distanceToCamera));
        }



        /// <summary>
        /// Gets the rect of the rectTransform in the UI space.
        /// UI space is defined in pixels. The bottom-left of the screen is (0,0); the right-top is (CanvasRealWidth, CanvasRealHeight).
        /// </summary>
        public Rect GetRectInUiSpace(RectTransform rectTransform)
        {
            Vector2 positionInUiSpace = (Vector2)transform.InverseTransformPoint(rectTransform.transform.position) + 
                                            new Vector2(CanvasRealWidth / 2f, CanvasRealHeight / 2f);

            Rect rect = rectTransform.rect;
            
            return new Rect(
                positionInUiSpace.x + rect.x, 
                positionInUiSpace.y + rect.y, 
                rect.width, 
                rect.height);
        }
        
        private void UpdateSafeArea()
        {
            RectInt screenRect = new RectInt(0, 0, Screen.width, Screen.height);
            RectInt safeArea = ComputeSafeArea();
            //safeArea = new RectInt(132, 63, Screen.width - 132, Screen.height - 63); // iPhone XS Safe Area

            UnityEngine.Debug.Log("Update Safe Area | Orientation: "+ _screenOrientation + " | Safe Area: " + safeArea + " | Screen Rect: " + screenRect);
            
            float left = (float)safeArea.x / screenRect.width;
            float right = (float)(safeArea.x + safeArea.width) / screenRect.width;
            
            float bottom = (float)safeArea.y / screenRect.height;
            float top = (float)(safeArea.y + safeArea.height) / screenRect.height;

            _canvasShiftValue = new Vector2(left, bottom);
            Vector2 anchorMax = new Vector2(right, top);
            
            Transform uiParent = _cs.transform.GetChild(0);
            if (uiParent != null)
            {
                ((RectTransform) uiParent).anchorMin = _canvasShiftValue;
                ((RectTransform) uiParent).anchorMax = anchorMax;
            }

            /* Update Reference Resolution */

            float ratioX = (float)screenRect.width / safeArea.width;
            float ratioY = (float)screenRect.height / safeArea.height;
            Vector2 newReferenceResolution = new Vector2(_baseReferenceResolution.x * ratioX, _baseReferenceResolution.y * ratioY);
            
            _cs.referenceResolution = newReferenceResolution;
        }

        private RectInt ComputeSafeArea()
        {
            Rect safeArea = Screen.safeArea;
            
            return new RectInt(
                (int)safeArea.x, 
                (int)safeArea.y, 
                (int)safeArea.width, 
                (int)safeArea.height);
        }

        private ScreenOrientation GetScreenOrientation()
        {
            #if UNITY_ANDROID
            return ScreenOrientation.LandscapeLeft;
            #else
            return Screen.orientation;
            #endif
        }
    }
} // namespace FranSTools.Essential