using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace MusicApi.Controllers {

    
    public class ArtistController : Controller {

        private List<Artist> allArtists;

        public ArtistController() {
            allArtists = JsonToFile<Artist>.ReadJson();
        }
        //This method is shown to the user navigating to the default API domain name
        //It just display some basic information on how this API functions
        [Route("")]
        [HttpGet]
        public string Index() {
            //String describing the API functionality
            string instructions = "Welcome to the Music API~~\n========================\n";
            instructions += "    Use the route /artists/ to get artist info.\n";
            instructions += "    End-points:\n";
            instructions += "       *Name/{string}\n";
            instructions += "       *RealName/{string}\n";
            instructions += "       *Hometown/{string}\n";
            instructions += "       *GroupId/{int}\n\n";
            instructions += "    Use the route /groups/ to get group info.\n";
            instructions += "    End-points:\n";
            instructions += "       *Name/{string}\n";
            instructions += "       *GroupId/{int}\n";
            instructions += "       *ListArtists=?(true/false)\n";
            return instructions;
        }
        //Create a route for /artists that returns all artists as JSON
        [HttpGet]
        [Route("/artists")]
        public JsonResult artists()
        {
            return Json(allArtists);
        }
        //Create a route /artists/name/{name} that returns all artists that match the provided name
        [HttpGet]
        [Route("artists/name/{name}")]
        public JsonResult artists_names(string name)
        {
            var artists_names = from artists in allArtists where artists.ArtistName.Contains(name) select artists;
            return Json(artists_names);
        }
        //Create a route /artists/realname/{name} that returns all artists who real names match the provided name
        [HttpGet]
        [Route("artists/realname/{name}")]
        public JsonResult real_names(string name)
        {
            var real_names = from artists in allArtists where artists.RealName.Contains(name) select artists;
            return Json(real_names);
        }
        //Create a route /artists/hometown/{town} that returns all artists who are from the provided town
        [HttpGet]
        [Route("artists/hometown/{hometown}")]
        public JsonResult home_town(string hometown)
        {
            var home_town = from artists in allArtists where artists.Hometown.Contains(hometown) select artists;
            return Json(home_town);
        }
        //Create a route /artists/groupid/{id} that returns all artists who have a GroupId that matches id
        [HttpGet]
        [Route("artists/groupid/{id}")]
        public JsonResult grouped_artists(string id)
        {
            var grouped_artists = from artists in allArtists where artists.GroupId.ToString() == id select artists;
            return Json(grouped_artists);
        }
    }
}