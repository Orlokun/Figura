using UnityEngine;
namespace UnityCore
{
    namespace Menu
    {
        public class TestMenu : MonoBehaviour
        {
            PageController pcontroller;

#if UNITY_EDITOR
            public  PageController pController;
            private void Update()
            {
                if (Input.GetKeyUp(KeyCode.F))
                {
                    pController.TurnPageOn(PageType.Loading);
                }
                if (Input.GetKeyUp(KeyCode.G))
                {
                    pController.TurnPageOff(PageType.Loading);

                }
            }
#endif
        }
    }
}


