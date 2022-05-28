using System.Collections;

namespace Core.Locator
{
    public interface IModule
    {
        /// <summary>
        /// Forms initial module's state and links all the necessary dependencies.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        IEnumerator Init(IApp app);
 
        /// <summary>
        /// The secondary initialization. The outer calls to others modules must be represented into this method.
        /// </summary>
        void Link();
    }
}
