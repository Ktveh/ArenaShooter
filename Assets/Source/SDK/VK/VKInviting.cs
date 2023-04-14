using DungeonGames.VKGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VKInviting : MonoBehaviour
{
    [SerializeField] private Button _inviting;
    [SerializeField] private GameObject _infoSuccess;
    [SerializeField] private GameObject _infoError;
    [SerializeField] private int _reward = 1000;

    public event UnityAction<int> Rewarded;
    public event UnityAction Error;

    private void Awake()
    {
#if VK_GAMES
        _inviting.gameObject.SetActive(true);
#endif
    }

    private void OnEnable()
    {
        _inviting.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _inviting.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        SocialInteraction.InviteFriends(OnRewarded, OnError);
    }

    private void OnRewarded()
    {
        Rewarded?.Invoke(_reward);
        _infoSuccess.SetActive(true);
    }

    private void OnError()
    {
        Error?.Invoke();
        _infoError.SetActive(true);
    }
}
