using Gofferwall;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GofferwallSdk Example 
/// </summary>
public class GofferwallExample : MonoBehaviour
{

    private string MEDIA_ID;
    private string USER_ID;
    private string OFFERWALL_ID;
    private float top;

    public GofferwallExample()
    {
#if UNITY_ANDROID
            MEDIA_ID = "";
            USER_ID = "";
            OFFERWALL_ID = "";
#endif

#if UNITY_IOS
            MEDIA_ID = "";
            USER_ID = "";
            OFFERWALL_ID = "";
#endif
    }

    // Gofferwall Interface
    private Gofferwall.Feature.Core core;
    private Gofferwall.Feature.OfferwallAd offerwallAd;

    // Properties
    private string outputMessage;
    private readonly int fontSize = 30;
    private List<ContentView> views;

    // Mapper
    class GofferwallItemFetcher {
        private static Dictionary<string, string> secretKeys = new Dictionary<string, string>() {
            { "", "" },
            { "", "" }
        };

        public static string FetchMediaScretKey(string mediaID) { return secretKeys[mediaID]; }
    }

    public Vector2 scrollPosition = Vector2.zero;

    private void Start() { this.core = Gofferwall.Sdk.GetCoreInstance(); }

    private void OnDisable()
    {
        // Unregister Gofferwall Callbacks
        this.offerwallAd.OnOpened -= OnOfferwallAdOpenedCallback;
        this.offerwallAd.OnFailedToShow -= OnOfferwallAdFailedToShowCallback;
    }

    private void RegisterGofferwallCallback()
    {
        if (this.offerwallAd == null)
        {
            this.offerwallAd = Gofferwall.Sdk.GetOfferwallAdInstance();
            this.offerwallAd.OnOpened += OnOfferwallAdOpenedCallback;
            this.offerwallAd.OnFailedToShow += OnOfferwallAdFailedToShowCallback;
        }
    }

    private void OnInitializedCallback(object sender, Gofferwall.Model.InitResult args) { this.AddOutputMessage("initialize - args: " + args); }
    private void OnOfferwallAdOpenedCallback(object sender, Gofferwall.Model.ShowResult args) { this.AddOutputMessage("  <= offerwallAd.OnOpened - args: " + args); }
    private void OnOfferwallAdFailedToShowCallback(object sender, Gofferwall.Model.ShowFailure args) { this.AddOutputMessage("  <= offerwallAd.OnFailedToShow - args: " + args); }

    #region GUI
    private void AddContentViews()
    {

        GUI.backgroundColor = Color.black;

        // Core
        this.AddLabel("Core");
        this.AddTextField("Media ID", TextFieldType.MediaID);
        this.AddTextField("User ID", TextFieldType.UserID);
        this.AddButton("Set User ID", () => {
            this.core.SetUserId(USER_ID);
            this.AddOutputMessage("Set User ID: " + USER_ID);
        });

        this.AddButton("Initialize", () =>
        {
            this.core.Initialize((isSuccess) =>
            {
                if (!isSuccess)
                {
                    this.AddOutputMessage("Initialized: " + isSuccess);
                    return;
                }

                this.RegisterGofferwallCallback();
                this.core.SetUserId(USER_ID);
                this.AddOutputMessage("Initialized: " + isSuccess + ", Setup UserID: " + USER_ID);
            });
        });

        this.AddButton("Initialize(mediaId, mediaSecret, listener)", () => {
            string secretKey = GofferwallItemFetcher.FetchMediaScretKey(MEDIA_ID);
            if (secretKey == null)
            {
                this.AddOutputMessage("Not Found SecretKey: " + MEDIA_ID);
                return;
            }

            this.core.Initialize(MEDIA_ID, secretKey, (isSuccess) => {
                if (!isSuccess)
                {
                    this.AddOutputMessage("Initialized: " + isSuccess);
                    return;
                }

                this.RegisterGofferwallCallback();
                this.core.SetUserId(USER_ID);
                this.AddOutputMessage("Initialized: " + isSuccess + ", Setup UserID: " + USER_ID);
            });
        });

        this.AddButton("is Initialized", () => {
            this.AddOutputMessage("Initialized Flag: " + Gofferwall.Sdk.GetCoreInstance().IsInitialized());
        });

        this.AddButton("Print SDK Version", () => {
            this.AddOutputMessage("SDK Versions => " + Gofferwall.Sdk.GetOptionGetter().GetSDKVersion());
        });

        this.AddButton("Print Unity SDK Version", () => {
            this.AddOutputMessage("SDK Versions => " + Gofferwall.Sdk.GetOptionGetter().GetUnitySDKVersion());
        });

        this.AddButton("Print Network Version", () => {
            this.AddOutputMessage("Network Versions => " + Gofferwall.Sdk.GetOptionGetter().GetNetworkVersions());
        });


        // Offerwall
        this.AddSpacer();
        this.AddTextField("Global Offerwall", TextFieldType.OfferwallUnit);
        this.AddButton("Global Show Offerwall", () => {
            string unitID = OFFERWALL_ID.ToUpper();
            if (unitID == null)
            {
                this.AddOutputMessage("Not Found unitID: " + MEDIA_ID);
                return;
            }

            if (this.offerwallAd.Show(unitID)) { } else { this.AddOutputMessage("offerwallAd.Show request is duplicated"); }
        });
        this.AddButton("TNK Show Offerwall", () => {
            string unitID = OFFERWALL_ID.ToUpper();
            if (unitID == null)
            {
                this.AddOutputMessage("Not Found unitID: " + MEDIA_ID);
                return;
            }

            if (this.offerwallAd.Show4TNK(unitID)) { } else { this.AddOutputMessage("offerwallAd.Show request is duplicated"); }
        });
        this.AddButton("Tapjoy Show Offerwall", () => {
            string unitID = OFFERWALL_ID.ToUpper();
            if (unitID == null)
            {
                this.AddOutputMessage("Not Found unitID: " + MEDIA_ID);
                return;
            }

            if (this.offerwallAd.Show4Tapjoy(unitID)) { } else { this.AddOutputMessage("offerwallAd.Show request is duplicated"); }
        });
    }

    private void OnGUI()
    {
        // Support Only Portrait
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float horizontalMargin = (float)(screenWidth * 0.03);                   // Horizon Margin Height
        float verticalMargin = (float)(screenHeight * 0.015);                   // Vertical Margin Height
#if UNITY_IOS
        top = verticalMargin * 3;
#endif

#if UNITY_ANDROID
        top = verticalMargin;
#endif
        float contentHeight = (float)(screenHeight * 0.3);
        Rect rect = new Rect(horizontalMargin, top, screenWidth - (horizontalMargin * 2), contentHeight);
        GUIStyle logTextStyle = new GUIStyle(GUI.skin.textArea);
        logTextStyle.fontSize = fontSize;
        GUI.Label(rect, this.outputMessage, logTextStyle);

        ClearContentViews();
        AddContentViews();

        contentHeight = (float)(50);                                            // Content Height : 5% for Screen Height
        top = rect.yMax + verticalMargin;

        Rect position = new Rect(0, top, Screen.width, Screen.height - top);
        Rect viewRect = new Rect(0, 0, Screen.width, 0);
        foreach (ContentView view in this.views)
        {
            viewRect.height += view.height;
            viewRect.height += verticalMargin;
        }

        GUI.skin.verticalScrollbar.fixedWidth = Screen.width * 0.04f;
        GUI.skin.verticalScrollbarThumb.fixedWidth = Screen.width * 0.04f;

        scrollPosition = GUI.BeginScrollView(
            position, scrollPosition, viewRect
        );

        top = 0;
        float contentWidth = screenWidth - (horizontalMargin * 2);
        foreach (ContentView view in this.views)
        {

            if (view.type == ContentViewType.Spacer)
            {
                rect = new Rect(0, 0, 0, view.height);

            }
            else if (view.type == ContentViewType.Button)
            {
                rect = new Rect(horizontalMargin, top, contentWidth, view.height);
                GUIStyle style = new GUIStyle(GUI.skin.button);
                style.fontSize = fontSize;
                if (GUI.Button(rect, view.title, style))
                {
                    this.AddOutputMessage("Button Tapped: " + view.title);
                    view.buttonAction();
                };

            }
            else if (view.type == ContentViewType.Label)
            {
                rect = new Rect(horizontalMargin, top, contentWidth, view.height);
                GUIStyle style = new GUIStyle(GUI.skin.textArea);
                style.fontSize = fontSize;
                GUI.Label(rect, view.title, style);

            }
            else if (view.type == ContentViewType.TextField)
            {
                float descriptionWidth = (float)(contentWidth * 0.33);          // Description Label Width 33%
                float textFieldWidth = (float)(contentWidth * 0.63);            // Text Field Width 63%
                                                                                // Margin 4%

                Rect descriptionRect = new Rect(horizontalMargin, top, descriptionWidth, view.height);
                GUIStyle style = new GUIStyle(GUI.skin.textArea);
                style.fontSize = fontSize;
                GUI.Label(descriptionRect, view.title, style);

                float maxX = horizontalMargin + descriptionWidth;               // Calulate Textfield MinX
                maxX += (float)(contentWidth - (descriptionWidth + textFieldWidth));

                rect = new Rect(maxX, top, textFieldWidth, view.height);

                switch (view.textFieldType)
                {
                    case TextFieldType.MediaID: MEDIA_ID = GUI.TextField(rect, MEDIA_ID, style); break;
                    case TextFieldType.UserID: USER_ID = GUI.TextField(rect, USER_ID, style); break;
                    case TextFieldType.OfferwallUnit: OFFERWALL_ID = GUI.TextField(rect, OFFERWALL_ID, style); break;
                }

            }


            top += rect.height;
            top += verticalMargin;
        }

        GUI.EndScrollView();
    }

    private void AddOutputMessage(string message)
    {
        if (this.outputMessage == null || this.outputMessage.Length == 0) { this.outputMessage = message; }
        else { this.outputMessage = message + "\n" + this.outputMessage; }
    }

    private void ClearContentViews()
    {
        if (this.views == null) { this.views = new List<ContentView>(); }
        else { this.views.Clear(); }
    }

    private void AddSpacer()
    {
        this.views.Add(new ContentView(ContentViewType.Spacer));
    }

    private void AddLabel(string title)
    {
        this.views.Add(new ContentView(ContentViewType.Label, title));
    }

    private void AddButton(string title, Action function)
    {
        this.views.Add(new ContentView(ContentViewType.Button, title, function));
    }

    private void AddTextField(string title, TextFieldType type)
    {
        this.views.Add(new ContentView(ContentViewType.TextField, title, type));
    }

#endregion
}

public enum ContentViewType { Button, TextField, Label, Spacer }
public enum TextFieldType { MediaID, UserID, OfferwallUnit, }

class ContentView
{
    public TextFieldType textFieldType;
    public ContentViewType type;
    public String title;
    public Action buttonAction;

    public ContentView(ContentViewType type) { this.type = type; }

    public ContentView(ContentViewType type, String title, Action action = null)
    {
        this.type = type;
        this.title = title;
        this.buttonAction = action;
    }

    public ContentView(ContentViewType type, String title, TextFieldType textFieldType)
    {
        this.type = type;
        this.title = title;
        this.textFieldType = textFieldType;
    }

    public float height
    {
        get
        {
            switch (this.type)
            {
                case ContentViewType.Button:
                    return 100;
                case ContentViewType.TextField:
                case ContentViewType.Label:
                    return 50;
                case ContentViewType.Spacer:
                    return 25;
                default:
                    return 0;
            }
        }
    }
}