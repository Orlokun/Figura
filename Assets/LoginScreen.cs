using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LoginScreen : MonoBehaviour
{
    #region GlobalVariables
    [SerializeField]
    GameObject[] uiElements;
    private KeyCode pressedKey;
    private int delayTime = 20;
    private int currentDelay = 0;

    #region LoginInputVariables
    [SerializeField]
    GameObject userMailObj;
    [SerializeField]
    GameObject userPassObj;

    [SerializeField]
    TMP_Text userMail;
    [SerializeField]
    TMP_Text userPass;

    EventSystem mEvent;
    #endregion

    string forcedId = "camilo.hernandez@edukativa.cl";
    string forcedPass = "pass123";

    string jLoginData;
    #endregion

    #region Awakefunctions
    private void Awake()
    {
        CheckBasicSettings();
    }

    void CheckBasicSettings()
    {
        mEvent = EventSystem.current;
        if (userMailObj == null || userPassObj == null)
        {
            Debug.LogError("Login Screen does not have the Text GameObjects set on script");
        }
        userMail = userMailObj.GetComponent<TMP_Text>();
        userPass = userPassObj.GetComponent<TMP_Text>();
        StaticGameManager.CreateServerInstance();
        StaticGameManager.sMessageHandler.SetApiUrl("https://api.edukativa.cl");
    }
    #endregion

    #region Updatefunctions

    private void Update()
    {
        CheckUserInput();
        CheckInputCorrectness();
    }

    void CheckUserInput()               //TODO: Make a more Scalable System and Set disabled button time
    {
        GameObject selectedObject = mEvent.currentSelectedGameObject;
        if (selectedObject != null)
        {
            int objIndex = GetActiveObjectIndex(selectedObject);
            if (Input.GetKey(KeyCode.Tab) && pressedKey != KeyCode.Tab)
            {
                pressedKey = KeyCode.Tab;
                SelectNextUIObject(objIndex);
            }
        }
    }
    #endregion

    #region Utils
    void SelectNextUIObject(int _objIndex)
    {
        if (_objIndex + 1 >= uiElements.Length)
        {
            _objIndex = 0;
        }
        else
        {
            _objIndex++;
        }

        mEvent.SetSelectedGameObject(uiElements[_objIndex]);
    }
    int GetActiveObjectIndex(GameObject _selObject)
    {
        for (int i = 0; i < uiElements.Length; i++)
        {
            if (uiElements[i] == _selObject)
            {
                return i;
            }
        }
        return 0;
    }
    void CheckInputCorrectness()
    {

    }
    public void SendLogin()
    {
        SetUserLoginParameters();
        StaticGameManager.sMessageHandler.SendMessageToServer(this, "login", jLoginData, "POST");
    }
    void SetUserLoginParameters()
    {
        StaticGameManager.pData = ScriptableObject.CreateInstance<PlayerProfileData>();
        StaticGameManager.pData.SetUserLogData(forcedId, forcedPass);                               //TODO: Check if Input is Safe. Set Real Data, not Forced
        jLoginData = JsonUtility.ToJson(StaticGameManager.pData.GetLogData());
        Debug.Log(jLoginData);
    }

    #endregion
}