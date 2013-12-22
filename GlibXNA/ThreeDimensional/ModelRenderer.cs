using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.ThreeDimensional
{
    /// <summary>
    /// A class that assists with the rendering of a 3D model.
    /// </summary>
    public class ModelRenderer : ISprite
    {
        private Model _model;

        /// <summary>
        /// Gets or sets the <see cref="Microsoft.Xna.Framework.Graphics.Model"/> to render.
        /// </summary>
        public Model Model
        {
            get { return _model; }
            set {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _model = value;
            }
        }

        private Vector3 _rotation;

        /// <summary>
        /// Gets or sets the rendering rotation of the model.
        /// </summary>
        public Vector3 Rotation
        {
            get { return _rotation; }
            set { _rotation = value; _calculatedWorldEffect = false; }
        }
        
        

        /// <summary>
        /// Create a new <see cref="ModelRenderer"/> to render the specified <see cref="Microsoft.Xna.Framework.Graphics.Model"/>.
        /// </summary>
        /// <param name="model">The model to render.</param>
        /// <param name="screen">The <see cref="Viewport"/> on which the <see cref="Microsoft.Xna.Framework.Graphics.Model"/> will be rendered.</param>
        public ModelRenderer(Model model, Viewport screen) : this(model, screen.AspectRatio)
        {
            
        }

        private float _aspectRatio;

        /// <summary>
        /// Gets or sets the aspect ratio of the <see cref="Viewport"/> on which the <see cref="Microsoft.Xna.Framework.Graphics.Model"/> will be rendered.
        /// </summary>
        /// <remarks>
        /// Recalculates the projection matrix using default values.
        /// </remarks>
        public float AspectRatio
        {
            get { return _aspectRatio; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("The aspect ratio must be greater than zero.");
                }
                _aspectRatio = value;
                _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                        MathHelper.ToRadians(45.0f), _aspectRatio,
                        1.0f, 10000.0f);
            }
        }
        

        /// <summary>
        /// Create a new <see cref="ModelRenderer"/> to render the specified <see cref="Microsoft.Xna.Framework.Graphics.Model"/>.
        /// </summary>
        /// <param name="model">The model to render.</param>
        /// <param name="aspectRatio">The aspect ratio of the <see cref="Viewport"/> on which the <see cref="Microsoft.Xna.Framework.Graphics.Model"/> will be rendered.</param>
        public ModelRenderer(Model model, float aspectRatio)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            _model = model;

            if (aspectRatio <= 0)
            {
                throw new ArgumentException("The aspect ratio must be greater than zero.");
            }
            AspectRatio = aspectRatio;
        }

        private Vector3 _position;

        /// <summary>
        /// Gets or sets the position of the model.
        /// </summary>
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; _calculatedWorldEffect = false; }
        }

        private Vector3 _cameraPos = new Vector3(0.0f, 50.0f, 5000.0f);

        /// <summary>
        /// Gets or sets of the position of the camera looking at the model.
        /// </summary>
        public Vector3 CameraPosition
        {
            get { return _cameraPos; }
            set { _cameraPos = value; }
        }

        /// <summary>
        /// A boolean indicating whether or not the most up-to-date world effect has been calculated.
        /// </summary>
        protected bool _calculatedWorldEffect = false;
        private Matrix _worldEffect;

        /// <summary>
        /// A boolean indicating whether or not the most up-to-date view effect has been calculated.
        /// </summary>
        protected bool _calculatedViewEffect = false;
        private Matrix _viewEffect;

        private Vector3 _cameraTarget = Vector3.Zero;

        /// <summary>
        /// Gets or sets the target position of the camera.
        /// </summary>
        public Vector3 CameraTarget
        {
            get { return _cameraTarget; }
            set { _cameraTarget = value; }
        }

        //private Matrix _fieldOfView;

        private Matrix _projectionMatrix;

        /// <summary>
        /// Gets or sets the projection matrix to use when rendering.
        /// </summary>
        public Matrix ProjectionMatrix
        {
            get { return _projectionMatrix; }
            set { _projectionMatrix = value; }
        }
        
        /// <summary>
        /// Renders this model to the GraphicsDevice.
        /// </summary>
        public void Draw()
        {
            Matrix[] transforms = new Matrix[_model.Bones.Count];
            _model.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in _model.Meshes)
            {
                // This is where the mesh orientation is set, as well 
                // as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    if (!_calculatedWorldEffect)
                    {
                        _worldEffect = transforms[mesh.ParentBone.Index] *
                        Matrix.CreateRotationX(_rotation.X) *
                        Matrix.CreateRotationY(_rotation.Y) *
                        Matrix.CreateRotationZ(_rotation.Z)
                        * Matrix.CreateTranslation(_position);
                        _calculatedWorldEffect = true;
                    }
                    effect.World = _worldEffect;

                    if (!_calculatedViewEffect)
                    {
                        _viewEffect = Matrix.CreateLookAt(_cameraPos,
                        _cameraTarget, Vector3.Up);
                        _calculatedViewEffect = true;
                    }
                    effect.View = _viewEffect;
                    effect.Projection = _projectionMatrix;
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }
        }

        /// <summary>
        /// When implemented in a subclass, updates the model.
        /// </summary>
        public virtual void Update()
        {
        //Does nothing
        }
    }
}
