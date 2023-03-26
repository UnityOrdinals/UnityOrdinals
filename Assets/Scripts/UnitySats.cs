using System;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    //Sats Connect for Unity <3
    public class UnitySats : MonoBehaviour
    {
        public Button getAddressButton;
        public TMP_Text paymentWallet;
        public TMP_Text ordinalsWallet;

        [DllImport("__Internal")]
        private static extern void SetupPlayer();
        
        [DllImport("__Internal")]
        private static extern void GetAddress();

        void Start()
        {
            Button loginPrefab = getAddressButton.GetComponent<Button>();
            loginPrefab.onClick.AddListener(GetWalletAddress);
            
            SetupPlayer();
        }
        
        private void GetWalletAddress()
        {
            Debug.Log("Requesting Wallet Access !");
            GetAddress();
        }
        
        
        private void HandleAddressResponse(string response)
        {
            try
            {
                var walletAddresses = JsonUtility.FromJson<Addresses>(response);
                ordinalsWallet.text = "Ordinals Wallet:" + walletAddresses.addresses[0].address;
                paymentWallet.text = "Payments Wallet:" + walletAddresses.addresses[1].address;
            }
            catch (Exception e)
            {
                Debug.LogError("Error while parsing addresses from wallet.");
                throw;
            }
            Debug.Log(response);
        }
    }