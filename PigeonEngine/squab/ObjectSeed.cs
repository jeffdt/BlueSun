using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Pigeon.Gfx;
using Pigeon.squab;
using PigeonEngine.gfx;

namespace PigeonEngine.squab {
	[Serializable]
	public class ObjectSeed {
		public string ParentName;

		public string Name;
		public float Layer;
		public Point Position;
		public string Sprite;
		public string Animation;
		public bool IsSpriteLooped;
		public bool IsSpritePingPonged;
		public bool IsAttached;	// TODO: delete?
		public bool IsUpdateEnabled = true;
		public bool IsDrawEnabled = true;

		public bool IsYLayerSorted = false;
		public int YLayerSortOffset;

		public List<ObjectSeed> Children; 

		public Squabject Build() {
			Squabject obj = new Squabject(Name, Layer);
			if (Sprite != null) {
				var spriteRenderer = new SpriteRenderer(Sprite);
				obj.AddComponent(spriteRenderer);
				if (Animation != null) {
					if (IsSpriteLooped) {
						spriteRenderer.Loop(Animation);
					} else if (IsSpritePingPonged) {
						spriteRenderer.PingPong(Animation);
					} else {
						spriteRenderer.Play(Animation);
					}
				}
			}

			obj.FlatLocalPosition = Position;
			obj.UpdateDisabled = !IsUpdateEnabled;
			obj.DrawDisabled = !IsDrawEnabled;
			if (IsYLayerSorted) {
				obj.AddComponent(new YSorter { YOffset = YLayerSortOffset });
			}

			if (Children != null) {
				foreach (var child in Children) {
					obj.AddChild(child.Build());
				}
			}

			return obj;
		}

		public ObjectSeed AddChild(string name, float layer, Point position, string sprite = null, string animation = null, bool isSpriteLooped = false) {
			var objectSeed = new ObjectSeed { Name = name, Layer = layer, Position = position, Sprite = sprite, Animation = animation, IsSpriteLooped = isSpriteLooped };
			return AddChild(objectSeed);
		}

		public ObjectSeed AddChild(ObjectSeed child) {
			if (Children == null) {
				Children = new List<ObjectSeed>();
			}

			Children.Add(child);
			return this;
		}
	}
}