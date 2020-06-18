using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EldritchDating.API.Data;
using EldritchDating.API.DTOs;
using EldritchDating.API.Helpers;
using EldritchDating.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EldritchDating.API.Controllers
{
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/users/{userId}/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IDatingRepository repo;
        public MessagesController(IDatingRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet("{messageId}", Name="GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int messageId) 
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var messageFromRepo = await repo.GetMessage(messageId);

            if (messageFromRepo == null)
                return NotFound();
            
            return Ok(messageFromRepo);
        }

        [HttpGet()]
        public async Task<IActionResult> GetMessages(int userId, [FromQuery]MessageParams messageParams) 
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            messageParams.UserId = userId;
            var messagesFromRepo = await repo.GetMessagesForUser(messageParams);
            var messages = mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            Response.AddPagination(
                messagesFromRepo.CurrentPage, 
                messagesFromRepo.PageSize, 
                messagesFromRepo.TotalCount, 
                messagesFromRepo.TotalPages);

            return Ok(messages);
        }

        [HttpGet("/thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(int userId, int recipientId) 
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messagesFromRepo = await repo.GetMessageThread(userId, recipientId);
            var messageThread = mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            return Ok(messageThread);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageForCreationDto messageForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            messageForCreationDto.SenderId = userId;

            var recipient = await repo.GetUser(messageForCreationDto.RecipientId);

            if (recipient == null)
                return BadRequest("Could not find user");

            var message = mapper.Map<Message>(messageForCreationDto);

            repo.Add(message);

            if (await repo.SaveAll())
            {
                var messageToReturn = mapper.Map<MessageForCreationDto>(message);
                return CreatedAtRoute("GetMessage", new { userId, messageId = message.Id}, messageToReturn);
            }

            throw new Exception("Failed to create message - error saving new message");
        }
    }
}