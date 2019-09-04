using System;

namespace pigeon.squab {
    public abstract class Component {
		public Squabject Object;

		public Action Destructor;
		private bool initialized;

        private bool enabled = true;
        public bool Enabled {
            get { return enabled; }
            set { enabled = value; }
        }

		public T GetComponent<T>() where T : Component {
		    return Object.GetComponent<T>();
	    }

	    internal void InitializeComponent() {
		    if (!initialized) {
			    Initialize();
		    }

		    initialized = true;
	    }

		internal void UpdateComponent() {
		    if (!enabled) {
			    return;
		    }

		    Update();
	    }

		protected abstract void Initialize();
	    protected abstract void Update();
    }
}
