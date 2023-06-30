using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Noise.Client
{
    public class ServerAPI
    {
        public class ServerResponse
        {
            public int statusCode {  get; set; }
            public string response { get; set; }
        }

        private static readonly HttpClient client = new HttpClient();
        private static readonly HttpClient httpClient = new HttpClient();
        public static async Task<ServerResponse> authUser(string username, string password)
        {            
            var userData = new Dictionary<string, string>
            {
                { "login", username },
                { "password", password },
            };

            var data = new FormUrlEncodedContent(userData);
            var response = await client.PostAsync(Config.apiURL + "user/authorize", data);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse() {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> authUserByToken(string token)
        {
            var userData = new Dictionary<string, string>
            {
                { "session_token", token },
            };

            var data = new FormUrlEncodedContent(userData);
            var response = await client.PostAsync(Config.apiURL + "user/authorize", data);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> registerUser(string username, string email, string password)
        {
            var userData = new Dictionary<string, string>
            {
                { "login", username },
                { "email", email },
                { "password", password },
            };

            var data = new FormUrlEncodedContent(userData);
            var response = await client.PostAsync(Config.apiURL + "user/register", data);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> fetchAllSongs()
        {
            var response = await client.GetAsync(Config.apiURL + "songs/fetch/all");
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> fetchSongById(int songId)
        {
            var response = await client.GetAsync(Config.apiURL + "songs/fetch/" + songId);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> searchSong(string term)
        {
            var response = await client.GetAsync(Config.apiURL + "songs/search/" + term);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> getArtistInfoByName(string artistName)
        {
            var response = await client.GetAsync(Config.apiURL + "artist/fetch/name/" + artistName);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> setArtistInfo(Artist artistInfo, string bannerPath)
        {
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                multipartFormContent.Add(new StringContent(Config.userInfo.session_token), name: "session_token");
                multipartFormContent.Add(new StringContent(artistInfo.username), name: "username");
                multipartFormContent.Add(new StringContent(artistInfo.description), name: "description");
                multipartFormContent.Add(new StringContent("" + artistInfo.genre), name: "genre_id");

                if (bannerPath.Length != 0)
                {
                    var thumbStreamContent = new StreamContent(File.OpenRead(bannerPath));
                    thumbStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    multipartFormContent.Add(thumbStreamContent, name: "thumbnail", fileName: "thumbnail.png");
                }

                var response = await client.PostAsync(Config.apiURL + "artist/edit/", multipartFormContent);
                var responseString = await response.Content.ReadAsStringAsync();

                ServerResponse serverResponse = new ServerResponse()
                {
                    statusCode = (int)response.StatusCode,
                    response = responseString,
                };

                return serverResponse;
            }
        }

        public static async Task<ServerResponse> getLastRelease(int artistId)
        {
            var response = await client.GetAsync(Config.apiURL + "songs/lastsong/" + artistId);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> fetchSongByArtistUID(int userid)
        {
            var response = await client.GetAsync(Config.apiURL + "studio/songlist/" + userid);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> fetchArtistByBelong(int userid)
        {
            var response = await client.GetAsync(Config.apiURL + "artist/fetch/userid/" + userid);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> playSongById(int songId)
        {
            var response = await client.GetAsync(Config.apiURL + "songs/fetch/" + songId);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> fetchGenreList()
        {
            var response = await client.GetAsync(Config.apiURL + "songs/genrelist/");
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> uploadSong(UploadData songUploadData)
        {
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                multipartFormContent.Add(new StringContent(Config.userInfo.session_token), name: "session_token");
                multipartFormContent.Add(new StringContent("" + songUploadData.songName), name: "song_name");
                multipartFormContent.Add(new StringContent("" + songUploadData.songGenreId), name: "genre_id");

                var thumbStreamContent = new StreamContent(File.OpenRead(songUploadData.thumbnailPath));
                thumbStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                multipartFormContent.Add(thumbStreamContent, name: "thumbnail", fileName: "thumbnail.png");

                var songStreamContent = new StreamContent(File.OpenRead(songUploadData.songPath));
                songStreamContent.Headers.ContentType = new MediaTypeHeaderValue("audio/mp3");
                multipartFormContent.Add(songStreamContent, name: "song", fileName: "song.mp3");

                var response = await client.PostAsync(Config.apiURL + "songs/upload/", multipartFormContent);
                var responseString = await response.Content.ReadAsStringAsync();

                ServerResponse serverResponse = new ServerResponse()
                {
                    statusCode = (int)response.StatusCode,
                    response = responseString,
                };

                return serverResponse;
            }
        }

        public static async Task<ServerResponse> editSong(UploadData songUploadData, int songId)
        {
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                multipartFormContent.Add(new StringContent(Config.userInfo.session_token), name: "session_token");
                multipartFormContent.Add(new StringContent("" + songId), name: "song_id");
                multipartFormContent.Add(new StringContent("" + songUploadData.songName), name: "song_name");
                multipartFormContent.Add(new StringContent("" + songUploadData.songGenreId), name: "genre_id");

                if (songUploadData.thumbnailPath.Length != 0)
                {
                    var thumbStreamContent = new StreamContent(File.OpenRead(songUploadData.thumbnailPath));
                    thumbStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    multipartFormContent.Add(thumbStreamContent, name: "thumbnail", fileName: "thumbnail.png");
                }

                if (songUploadData.songPath.Length != 0)
                {
                    var songStreamContent = new StreamContent(File.OpenRead(songUploadData.songPath));
                    songStreamContent.Headers.ContentType = new MediaTypeHeaderValue("audio/mp3");
                    multipartFormContent.Add(songStreamContent, name: "song", fileName: "song.mp3");
                }

                var response = await client.PostAsync(Config.apiURL + "songs/edit/", multipartFormContent);
                var responseString = await response.Content.ReadAsStringAsync();

                ServerResponse serverResponse = new ServerResponse()
                {
                    statusCode = (int)response.StatusCode,
                    response = responseString,
                };

                return serverResponse;
            }
        }

        public static async Task<ServerResponse> deleteSong(int songId)
        {
            var userData = new Dictionary<string, string>
            {
                { "session_token", Config.userInfo.session_token },
                { "song_id", "" + songId },
            };

            var data = new FormUrlEncodedContent(userData);
            var response = await client.PostAsync(Config.apiURL + "songs/delete", data);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }


        public static async Task<ServerResponse> getPlaylists(int authorid)
        {
            var response = await client.GetAsync(Config.apiURL + "playlist/fetch/" + authorid);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> getPlaylistById(int playlistId)
        {
            var response = await client.GetAsync(Config.apiURL + "playlist/info/" + playlistId);
            var responseString = await response.Content.ReadAsStringAsync();

            ServerResponse serverResponse = new ServerResponse()
            {
                statusCode = (int)response.StatusCode,
                response = responseString,
            };

            return serverResponse;
        }

        public static async Task<ServerResponse> editPlaylist(Playlist playlist)
        {
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                multipartFormContent.Add(new StringContent(Config.userInfo.session_token), name: "session_token");
                multipartFormContent.Add(new StringContent("" + playlist.id), name: "playlistId");
                multipartFormContent.Add(new StringContent("" + playlist.name), name: "name");
                multipartFormContent.Add(new StringContent("" + playlist.description), name: "description");

                if (playlist.songs_id.Length != 0)
                {
                    
                }

                if (playlist.playlistThumb.Length != 0)
                {
                    var thumbStreamContent = new StreamContent(File.OpenRead(playlist.playlistThumb));
                    thumbStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    multipartFormContent.Add(thumbStreamContent, name: "thumbnail", fileName: "thumbnail.png");
                }

                var response = await client.PostAsync(Config.apiURL + "playlist/edit/" + playlist.id, multipartFormContent);
                var responseString = await response.Content.ReadAsStringAsync();

                ServerResponse serverResponse = new ServerResponse()
                {
                    statusCode = (int)response.StatusCode,
                    response = responseString,
                };

                return serverResponse;
            }
        }
    }
}
