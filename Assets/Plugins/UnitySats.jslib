var UnitySats = {
    SetupPlayer: function () {

        window.bs64escape = function (string) {
            return string.replace(/\+/g, "-").replace(/\//g, "_").replace(/=/g, "");
        }

        window.bs64encode = function (data) {
            if (typeof data === "object") {
                data = JSON.stringify(data);
            }
            return window.bs64escape(btoa(data));
        }
    },

    GetAddress: async function (message = "Ordinals Unity SDK Demo") {
        const provider = window.BitcoinProvider;

        if (!provider) {
            alert('No Bitcoin Wallet installed.');
            return;
        }

        let bs64header = window.bs64encode({
            alg: "none",
            typ: "JWT"
        });

        let bs64payload = window.bs64encode({
            purposes: ['ordinals', 'payment'],
            message: "Ordinals Unity SDK Demo",
            network: {
                type: 'Mainnet'
            },
        });

        let jwt = bs64header + "." + bs64payload + '.';
        const callResponse = await provider.connect(jwt);

         try {
            window.unityInstance.SendMessage("SatsButton", "HandleAddressResponse", JSON.stringify(callResponse));
        } catch {
            unityInstance.SendMessage("SatsButton", "HandleAddressResponse", JSON.stringify(callResponse));
        }
        
    },
};

mergeInto(LibraryManager.library, UnitySats);