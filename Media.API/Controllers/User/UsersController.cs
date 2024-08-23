using ErrorOr;
using Media.Application.User.Commands;
using Media.Application.User.Commands.FollowUser;
using Media.Application.User.Commands.UnFollowUser;
using Media.Application.User.Common;
using Media.Application.User.Queries;
using Media.Application.User.Queries.GetFollowingList;
using Media.Application.User.Queries.GetProfileUser;
using Media.Application.User.Queries.SearchUsers;
using Media.Contracts.Follower;
using Media.Contracts.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;

namespace Media.API.Controllers.User
{
    [Route("users")]
    public class UsersController : ApiController
    {
        private readonly ISender _meidator;

        public UsersController(ISender meidator)
        {
            _meidator = meidator;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var header = HttpContext.Request.Headers["Authorization"].ToString();

            var token = header.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var userId = jsonToken.Claims.First(claim => claim.Type == "sub").Value;

            var query = new GetCurrentQuery()
            {
                UserId = Guid.Parse(userId)
            };

            ErrorOr<CurrentUser> result = await _meidator.Send(query);

            return result.Match(
                result => Ok(UserResult(result)),
                errors => Problem(errors));
        }

        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetProfileUser(Guid userId)
        {
            var query = new GetProfileUserQuery()
            {
                UserId = userId
            };

            var result = await _meidator.Send(query);

            return result.Match(
                result => Ok(UserResult(result)),
                errors => Problem(errors));
        }

        private object? UserResult(CurrentUser result)
        {
            var userResult = new CurrentUserResponse()
            {
                UserId = result.UserId,
                UserName = result.UserName,
                Decription = result.Decription,
                DateOfBirth = result.DateOfBirth,
                PhoneNumber = result.PhoneNumber,
                Address = result.Address,
                Job = result.Job,
                PersonalImage = result.PersonalImage,
                BackgroundImage = result.BackgroundImage,
                AccountName = result.AccountName,
                AcctiveStatus = result.AcctiveStatus,
                UserRole = result.UserRole,
                CreatedAt = result.CreatedAt,
            };

            return userResult;
        }

        [HttpPatch("profile")]
        public async Task<IActionResult> UpdateProfile(ProFileRequest proFileRequest)
        {
            var command = new UpdateProfileCommand();
            command.UserId = proFileRequest.UserId;
            command.UserName = proFileRequest.UserName;
            command.Decription = proFileRequest.Decription;
            command.DateOfBirth = proFileRequest.DateOfBirth;
            command.PhoneNumber = proFileRequest.PhoneNumber;
            command.Address = proFileRequest.Address;
            command.Job = proFileRequest.Job;
            command.PersonalImage = proFileRequest.PersonalImage;
            command.BackgroundImage = proFileRequest.BackgroundImage;

            ErrorOr<UpdateProfileResult> updateResult = await _meidator.Send(command);

            return updateResult.Match(
                updateResult => Ok(ListResult(updateResult)),
                errors => Problem(errors));
        }

        [HttpGet("followers/{userId}")]
        public async Task<IActionResult> GetFollowingList(Guid userId)
        {
            var query = new GetFollowingListQuery()
            {
                UserId = userId,
            };

            var followerList = await _meidator.Send(query);

            return followerList.Match(
                followerList => Ok(FollowerList(followerList)),
                errors => Problem(errors));
        }

        [HttpPost("followers")]
        public async Task<IActionResult> FollowUser(FollowerRequest request)
        {
            var command = new FollowUserCommand()
            {
                UserId= request.UserId,
                FollowedId = request.FollowedId,
            };

            var result = await _meidator.Send(command);

            return result.Match(
                result => Ok(),
                errors => Problem(errors));
        }

        [HttpDelete("followers")]
        public async Task<IActionResult> UnFollowUser(FollowerRequest request)
        {
            var command = new UnFollowCommand()
            {
                UserId = request.UserId,
                FollowedId= request.FollowedId,
            };

            var result = await _meidator.Send(command);

            return result.Match(
                result => Ok(),
                errors => Problem(errors));
        }

        [HttpGet("followers/search")]
        public async Task<IActionResult> SearchUsers(string username)
        {
            var query = new SearchUsersQuery()
            {
                UserName = username,
            };

            ErrorOr<List<CurrentUser>> result = await _meidator.Send(query);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        private object? FollowerList(List<FollowerResult> followerList)
        {
            var list = new List<FollowerResult>();

            foreach (var result in followerList)
            {
                list.Add(result);
            }

            return list;
        }

        private object? ListResult(UpdateProfileResult updateResult)
        {
            var result = new ProfileRespone()
            {
                UserName = updateResult.UserName,
                Decription = updateResult.Decription,
                DateOfBirth = updateResult.DateOfBirth,
                PhoneNumber = updateResult.PhoneNumber,
                Address = updateResult.Address,
                Job = updateResult.Job,
                PersonalImage = updateResult.PersonalImage,
                BackgroundImage = updateResult.BackgroundImage,
            };

            return result;
        }
    }
}
