


using ModelsDAL = DAL.Models;
using DAL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WEBAPI_DotNetCore.Controllers
{
    [ApiController]
    [Route("Todo")]
    public class TodoController : ControllerBase
    {
        TodoRepository DalTodo = new TodoRepository();

        [HttpGet]
        [Route("Gets/{user_id}")]
        public List<ModelsDAL.Todo> Gets(int user_id)
        {
            return DalTodo.GetAll(user_id);
        }

        [HttpGet]
        [Route("Gets")]
        public List<ModelsDAL.Todo> Gets()
        {
            return DalTodo.GetAll();
        }


        [HttpGet]
        [Route("Get/{user_id:int}/{id:int}")]
        public ModelsDAL.Todo Get(int user_id, int id)
        {
            ModelsDAL.Todo OneTodo = DalTodo.Get(user_id, id);
            return OneTodo;
        }

        [HttpPost]
        [Route("Post")]
        public void Post(ModelsDAL.Todo todo)
        {
            DalTodo.Create(todo);
        }


        [HttpPut]
        [Route("Edit")]
        public void Edit(ModelsDAL.Todo todo)
        {
            DalTodo.Update(todo);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public void Delete(int id)
        {
            DalTodo.Delete(id);
        }
    }
}