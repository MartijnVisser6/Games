using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    public class Scene
    {
        public Vector2 Gravity { get; set; }

        List<GameObject> _mGameObjectsDynamic;
        List<GameObject> _mGameObjectsStatic;
        
        public Scene()
        {
            _mGameObjectsDynamic = new List<GameObject>();
            _mGameObjectsStatic = new List<GameObject>();
        }

        public void Add(GameObject gameObject)
        {
            if(gameObject.ObjectType == ObjectType.Dynamic)
                _mGameObjectsDynamic.Add(gameObject);
            if (gameObject.ObjectType == ObjectType.Static)
                _mGameObjectsStatic.Add(gameObject);
        }

        public void Add(List<GameObject> gameObjects)
        {
            foreach (GameObject obj in gameObjects)
                Add(obj);            
        }        

        public void Remove(GameObject gameObject)
        {
            if (gameObject.ObjectType == ObjectType.Dynamic)
                _mGameObjectsDynamic.Remove(gameObject);
            if (gameObject.ObjectType == ObjectType.Static)
                _mGameObjectsStatic.Remove(gameObject);
        }

        public void Remove(List<GameObject> gameObjects)
        {
            foreach (GameObject obj in gameObjects)
                Remove(obj);
        }

        public void Update(GameTime gameTime)
        {
            foreach(GameObject gameObject in _mGameObjectsDynamic)
            {
                gameObject.PreUpdate(gameTime);
                gameObject.Update(gameTime);
            }
            foreach (GameObject gameObject in _mGameObjectsStatic)
            {
                gameObject.PreUpdate(gameTime);
                gameObject.Update(gameTime);
            }
            
            HandleCollisions();
            UpdateGravity();
            GenerateCollisionLists();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in _mGameObjectsDynamic)
            {
                gameObject.Draw(spriteBatch);
            }
            foreach (GameObject gameObject in _mGameObjectsStatic)
            {
                gameObject.Draw(spriteBatch);
            }
        }

        private void UpdateGravity()
        {     
            foreach (GameObject gameObject in _mGameObjectsDynamic)
            {
                gameObject.IsOnGround = false;

                if (!CheckColissionDirectionally(gameObject, new Vector2(gameObject.Velocity.X + Gravity.X, 0)))
                    gameObject.Velocity += new Vector2(Gravity.X, 0);
                else
                    gameObject.Velocity = new Vector2(0, gameObject.Velocity.Y);

                if (!CheckColissionDirectionally(gameObject, new Vector2(0, gameObject.Velocity.Y + Gravity.Y)))
                    gameObject.Velocity += new Vector2(0, Gravity.Y);
                else
                    gameObject.Velocity = new Vector2(gameObject.Velocity.X, 0);

                if (CheckColissionDirectionally(gameObject, new Vector2(gameObject.Velocity.X + Gravity.X, gameObject.Velocity.Y + Gravity.Y)))
                    gameObject.IsOnGround = true;
            }  
        }

        private void HandleCollisions()
        {
            foreach (GameObject objDynamic in _mGameObjectsDynamic)
            {
                foreach (GameObject objStatic in _mGameObjectsStatic)
                {
                    Rectangle objDynamicBoundingBox = objDynamic.GetBoundingBox();
                    Rectangle objStaticBoundingBox = objStatic.GetBoundingBox();
                    if (objDynamicBoundingBox.Intersects(objStaticBoundingBox))
                    {
                        Vector2 positionVector = objDynamic.PreviousPosition - objDynamic.Position;
                        float xDiff = 0f;
                        float yDiff = 0f;
                        float playerBoundingBoxX = 0, playerBoundingBoxY = 0;
                        float platformBoundingBoxX = 0, platformBoundingBoxY = 0;

                        if (positionVector.X > 0)
                        {
                            playerBoundingBoxX = objDynamicBoundingBox.Left;
                            platformBoundingBoxX = objStaticBoundingBox.Right;
                        }
                        else if (positionVector.X < 0)
                        {
                            playerBoundingBoxX = objDynamicBoundingBox.Right;
                            platformBoundingBoxX = objStaticBoundingBox.Left;
                        }

                        if (positionVector.Y > 0)
                        {
                            playerBoundingBoxY = objDynamicBoundingBox.Top;
                            platformBoundingBoxY = objStaticBoundingBox.Bottom;
                        }
                        else if (positionVector.Y < 0)
                        {
                            playerBoundingBoxY = objDynamicBoundingBox.Bottom;
                            platformBoundingBoxY = objStaticBoundingBox.Top;
                        }

                        xDiff = platformBoundingBoxX - playerBoundingBoxX;
                        yDiff = platformBoundingBoxY - playerBoundingBoxY;

                        float xDiv = float.MaxValue;
                        float yDiv = float.MaxValue;

                        if (xDiff != 0)
                            xDiv = xDiff / positionVector.X;
                        if (yDiff != 0)
                            yDiv = yDiff / positionVector.Y;

                        if (xDiv < yDiv)
                        {
                            if (xDiff < 0)
                                objDynamic.Position = new Vector2(objStaticBoundingBox.Left - objDynamic.Texture.Width, objDynamic.Position.Y);
                            else if (xDiff > 0)
                                objDynamic.Position = new Vector2((objStaticBoundingBox.Right), objDynamic.Position.Y);
                        }
                        else
                        {
                            if (yDiff < 0)
                                objDynamic.Position = new Vector2(objDynamic.Position.X, objStaticBoundingBox.Top - objDynamic.Texture.Height);
                            else if (yDiff > 0)
                                objDynamic.Position = new Vector2(objDynamic.Position.X, objStaticBoundingBox.Bottom);
                        }
                    }
                }
            }
        }       

        private bool CheckColissionDirectionally(GameObject obj, Vector2 direction)
        {
            Rectangle objBoundingBox = obj.GetBoundingBox();

            if (direction.X > 0)
                objBoundingBox.Offset(1, 0);
            else if(direction.X < 0)
                objBoundingBox.Offset(-1, 0);

            if (direction.Y > 0)
                objBoundingBox.Offset(0, 1);
            else if (direction.Y < 0)
                objBoundingBox.Offset(0, -1);

            foreach(GameObject staticObj in _mGameObjectsStatic)
            {
                if (staticObj.GetBoundingBox().Intersects(objBoundingBox))
                    return true;
            }

            return false;
        }

        private void GenerateCollisionLists()
        {
            foreach(GameObject obj in _mGameObjectsDynamic)
            {
                obj.Collisions.Clear();

                foreach (GameObject obj2 in _mGameObjectsDynamic)
                {
                    if(obj != obj2)
                    {
                        if (obj.CheckCollision(obj2))
                        {
                            obj.Collisions.Add(obj2);
                        }
                    }
                }
            }
        }
    }
}
