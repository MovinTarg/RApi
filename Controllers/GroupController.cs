using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace MusicApi.Controllers {
    public class GroupController : Controller {
        List<Group> allGroups {get; set;}
        List<Artist> allArtists {get; set;}
        public GroupController() {
            allGroups = JsonToFile<Group>.ReadJson();
            allArtists = JsonToFile<Artist>.ReadJson();
        }
        //Create a route /groups that returns all groups as JSON
        [HttpGet]
        [Route("/groups")]
        public JsonResult groups()
        {
            var groups_with_members = allGroups.GroupJoin(allArtists, group => group.Id, artist => artist.GroupId,
                         (joined_group, joined_artists) => {
                                 joined_group.Members = joined_artists.ToList();
                                 return joined_group;
                             });
            return Json(groups_with_members);
        }
        //Create a route /groups/name/{name} that returns all groups that match the provided name
        [HttpGet]
        [Route("groups/name/{name}")]
        public JsonResult group_names(string name)
        {
            var groups_with_members = allGroups.GroupJoin(allArtists, group => group.Id, artist => artist.GroupId,
                         (joined_group, joined_artists) => {
                                 joined_group.Members = joined_artists.ToList();
                                 return joined_group;
                             });
            var group_names = from groups in groups_with_members where groups.GroupName.Contains(name) select groups;
            return Json(group_names);
        }
        //Create a route /groups/id/{id} that returns all groups with the provided Id value
        [HttpGet]
        [Route("groups/id/{id}")]
        public JsonResult group_by_id(string id)
        {
            var groups_with_members = allGroups.GroupJoin(allArtists, group => group.Id, artist => artist.GroupId,
                         (joined_group, joined_artists) => {
                                 joined_group.Members = joined_artists.ToList();
                                 return joined_group;
                             });
            var group_by_id = from groups in groups_with_members where groups.Id.ToString() == id select groups;
            return Json(group_by_id);
        }
    }
}