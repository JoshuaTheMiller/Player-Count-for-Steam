using System;
using System.Threading;
using System.Threading.Tasks;
using Trfc.ClientFramework;

namespace Trfc.SteamStats.ClientServices.GamePictures
{
    public sealed class GamePictureFetcher : IGamePictureFetcher
    {
        private readonly string connectionStringKey = "GamePicture";

        private readonly IConfigurationProvider configurationProvider;
        private readonly IWebGateway webGateway;

        public GamePictureFetcher(IConfigurationProvider configurationProvider, IWebGateway webGateway)
        {
            this.configurationProvider = configurationProvider;
            this.webGateway = webGateway;
        }

        public async Task<FetchGamePictureResponse> FetchPictureForGameAsync(int id, CancellationToken token)
        {
            var endpoint = configurationProvider.GetConnectionStringById(connectionStringKey);

            endpoint += $"?appId={id}";

            var gatewayResponse = await webGateway.GetResponseFromEndpoint<ResponseDao>(endpoint, token);

            if(gatewayResponse.ResultCode != WebRequestResponseResultCode.Succeeded)
            {
                return FetchGamePictureResponse.FetchFailed(gatewayResponse.ResultMessage);
            }

            if(!gatewayResponse.Succeeded)
            {
                return FetchGamePictureResponse.NoPicture(id);
            }

            var response = gatewayResponse.Value;

            if(response.IsValid)
            {
                var imageString = response.Image.TrimStart('b', '\'');
                imageString = imageString.TrimEnd('\'');

                var imageBytes = Convert.FromBase64String(imageString);
                return FetchGamePictureResponse.ContainsPicture(id, imageBytes);
            }

            return FetchGamePictureResponse.NoPicture(id);
        }

        [Preserve(AllMembers = true)]
        private sealed class ResponseDao
        {            

            public string Image { get; }
            public bool IsValid { get; }
            public string Message { get; }
            public ResponseDao(string image, bool isValid, string message)
            {
                Image = image;
                IsValid = isValid;
                Message = message;
            }
        }
    }
}
