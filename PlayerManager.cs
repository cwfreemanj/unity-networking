using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;

using System.Collections;

namespace Com.PhoxPhenom.SpriteMiner
{
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        PhotonView photonView;
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        public float speed = .1f;

        void Awake()
        {
            photonView = GetComponent<PhotonView>();
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }

            if (photonView.IsMine)
            {
                transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") *speed, 0);
            }
        }
        #region IPunObservable implementation


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
        }


        #endregion

    }
}
