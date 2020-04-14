using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using DAL.Services;
using ModelsDAL = DAL.Models;
using ModelsAPI = WEBAPI_DotNetCore.Models;
using WEBAPI_DotNetCore.Mappers;
using WEBAPI_DotNetCore.Utils.Security.RSA_Cryptography;

namespace WEBAPI_DotNetCore.Controllers
{
    [Route("UserTodo")]
    [ApiController]
    public class UserTodoController : ControllerBase
    {
        UserTodoRepository DalUser = new UserTodoRepository();
        KeyGenerator _keyGenerator;
        public UserTodoController(KeyGenerator keyGenerator)
        {
            _keyGenerator = keyGenerator;
        }

        [HttpGet]
        [Route("Gets")]
        public List<ModelsDAL.UserTodo> Gets()
        {
            return DalUser.GetAll();
        }


        [HttpGet]
        [Route("Get/{id:int}")]
        public ModelsDAL.UserTodo Get(int id)
        {
            ModelsDAL.UserTodo OneUser = DalUser.Get(id);
            return OneUser;
        }


        [HttpPost]
        [Route("Login")]
        public ModelsAPI.LoginTodoInfos Login(ModelsDAL.UserTodo user)
        {
            string PrivateKey = _keyGenerator.PrivateKey;
            user.Password = Decrypting.Decrypt(user.Password, PrivateKey);


            ModelsAPI.LoginTodoInfos OneUser = DalUser.Login(user).ToAPI();
            return OneUser;
        }


        [HttpPost]
        [Route("Register")]
        public void Register(ModelsDAL.UserTodo user)
        {

            string PrivateKey = _keyGenerator.PrivateKey;
            user.Password = Decrypting.Decrypt(user.Password, PrivateKey);

            DalUser.Create(user);
        }


        [HttpGet]
        [Route("GetPublicKey")]
        public string GetPublicKey()
        {
            _keyGenerator.GenerateKeys(RSAKeySize.Key1024);
            string publicKey = _keyGenerator.PublicKey;

            return publicKey;
        }





        [HttpPost]
        [Route("Post")]
        public void Post(ModelsDAL.UserTodo user)
        {
            DalUser.Create(user);
        }

        [HttpPut]
        [Route("Edit")]
        public void Edit(ModelsDAL.UserTodo user)
        {
            DalUser.Update(user);
        }


        [HttpDelete]
        [Route("Delete/{id:int}")]
        public void Delete(int id)
        {
            DalUser.Delete(id);
        }
    }
}

