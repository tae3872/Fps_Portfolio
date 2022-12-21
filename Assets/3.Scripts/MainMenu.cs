using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class MainMenu : MonoBehaviour
{
    public SceneFader fader;
    public string loadToScene = "MainScene001";
    public GameObject menu;
    public GameObject credit;
    public GameObject optionUI;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public AudioMixer audioMixer;
    public GameObject loadGame;

    bool isCreditShow = false;

    void Start()
    {
        InitGameData();
        AudioManager.instance.PlayBgm("MenuMusic");
        if (PlayerStats.instance.sceneNum > 1)
        {
            loadGame.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Update()
    {
        if (isCreditShow)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HideCredits();
            }
            return;
        }
    }
    void InitGameData()
    {
        LoadOptions();
        PlayData pData = SaveLoad.LoadData();
        PlayerStats.instance.InitPlayerStats(pData);
    }
    public void NewGame()
    {
        PlayerStats.instance.NewStartGameData();
        AudioManager.instance.Play("ButtonHit");
        AudioManager.instance.StopBgm();
        fader.FadeTo(loadToScene);
    }
    public void LoadGame()
    {
        AudioManager.instance.StopBgm();
        AudioManager.instance.Play("ButtonHit");
        fader.FadeTo(PlayerStats.instance.sceneNum);
    }
    public void Options()
    {
        menu.SetActive(false);
        optionUI.SetActive(true);
    }
    public void SetBgmVolume(float value)
    {
        audioMixer.SetFloat("BgmVolume", value);
    }
    public void SetSfxVolume(float value)
    {
        audioMixer.SetFloat("SfxVolume", value);
    }
    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", value);
    }
    public void Credits()
    {
        isCreditShow = true;
        menu.SetActive(false);
        credit.SetActive(true);
    }
    void HideCredits()
    {
        isCreditShow = false;
        menu.SetActive(true);
        credit.SetActive(false);
    }
    public void OptionExit()
    {
        SaveOptions();
        menu.SetActive(true);
        optionUI.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    void SaveOptions()
    {
        PlayerPrefs.SetFloat("SfxVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("BgmVolume", bgmSlider.value);
    }
    void LoadOptions()
    {
        float bgm = PlayerPrefs.GetFloat("BgmVolume", 0f);
        SetBgmVolume(bgm);
        bgmSlider.value = bgm;
        float sfx = PlayerPrefs.GetFloat("SfxVolume", 0f);
        SetSfxVolume(sfx);
        sfxSlider.value = sfx;
    }
}
