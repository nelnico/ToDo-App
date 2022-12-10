using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Common.Extensions;
using TodoApp.Core.Common.Pagination;
using TodoApp.Core.Dtos.Todos;
using TodoApp.Core.Entities;
using TodoApp.Core.Services;

namespace TodoApp.Api.Controllers;

[Authorize(Policy = "RequireUserRole")]
public class TodoController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly UserManager<AppUser> _userManager;

    public TodoController(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
    {
        _uow = uow;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<TodoItemDto>>> Get([FromQuery] TodoSearchParams searchParams)
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        var data = await _uow.TodoItems.SearchForUserAsync(user, searchParams);
        Response.AddPaginationHeader(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> Get(int id)
    {
        var todoItem = await _uow.TodoItems.GetByIdAsync(id);
        if (todoItem == null) return NotFound();

        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (todoItem.AppUser != user) return BadRequest("This is not your Todo Item!");

        var result = _mapper.Map<TodoItem, TodoItemDto>(todoItem);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Add(TodoEditDto dto)
    {
        var todoItem = _mapper.Map<TodoEditDto, TodoItem>(dto);
        todoItem.AppUser = await _userManager.GetUserAsync(HttpContext.User);

        _uow.TodoItems.Add(todoItem);
        await _uow.CompleteAsync();

        var result = _mapper.Map<TodoItem, TodoItemDto>(todoItem);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TodoItemDto>> Update(int id, TodoEditDto dto)
    {
        var todoItem = await _uow.TodoItems.GetByIdAsync(id);
        if (todoItem == null) return NotFound();

        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (todoItem.AppUser != user) return BadRequest("This is not your Todo Item!");

        _mapper.Map(dto, todoItem);
        _uow.TodoItems.Update(todoItem);
        await _uow.CompleteAsync();

        var result = _mapper.Map<TodoItem, TodoItemDto>(todoItem);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var todoItem = await _uow.TodoItems.GetByIdAsync(id);
        if (todoItem == null) return NotFound();

        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (todoItem.AppUser != user) return BadRequest("This is not your Todo Item!");

        _uow.TodoItems.Delete(todoItem);
        await _uow.CompleteAsync();

        return Ok();
    }
}