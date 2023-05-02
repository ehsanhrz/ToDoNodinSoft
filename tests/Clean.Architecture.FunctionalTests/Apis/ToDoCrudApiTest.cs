using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.ToDoAggregate;
using Clean.Architecture.Web.DTOs;
using FastEndpoints;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Clean.Architecture.FunctionalTests.Apis;

[Collection("Sequential")]
public class ToDoCrudApiTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client;
  public ToDoCrudApiTest(CustomWebApplicationFactory<Program> factory)
  {
    _client = factory.CreateClient();
    

  }

  //public async  Task<string?> GetTokenForTest()
  //{
  //  var LoginDto = new LoginDTO();
  //  LoginDto.username = "string";
  //  LoginDto.password = "string";

  //  var jsonContent = new StringContent(JsonConvert.SerializeObject(LoginDto), Encoding.UTF8, "application/json");

  //  var response = await _client.PostAsync($"api/Login/Login", jsonContent);

  //  var content = response.Content.As<TestToken>();
  //  //var token = JArray.Parse(content)[0].As<TestToken>;
    
  //  return content.token;
  //}

  [Fact]
  public async Task GetUserTodosTest()
  {
    // because the userID is unique and this id is not exists in the databse 
    // RDBMS will return null because there is no recorde

    Guid userID = Guid.NewGuid();

    var response = await _client.GetAsync($"api/ToDoCrud/GetUserToDos?userId={userID}");


    Assert.Equal(404, ((int)response.StatusCode));
  }

  [Fact]
  public async Task CreateUserToDosTest()
  {

    // because the userID is unique and this id is not exists in the databse 
    // RDBMS will return foregin key error so 400 but without JWT will return 401
    // and because this is test envirement wi will get 204
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjIyZWFmMjUxLWQ1MWMtNGQxYS04YjliLTllYWU5MWUzN2YxNCIsIm5hbWUiOiJzdHJpbmciLCJpc3MiOiJub2RpblNvZnQiLCJhdWQiOiJub2RpbnNvZnQiLCJuYmYiOjE2ODMwMjU5MDQsImV4cCI6MTY4NDMyMTkwNCwiaWF0IjoxNjgzMDI1OTA0fQ.tOp5E-HdZk7doqlctvkdjcvVc95n0A2dlVckL4uyGtU";
    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    var userID = Guid.NewGuid();
    var ToDoTile = "salam";
    var ToDoDescription = "test";

    var todo = new ToDo(userID, ToDoTile, ToDoDescription);

    var listToDo = new List<ToDo>() { todo };

    var jsonContent = new StringContent(JsonConvert.SerializeObject(listToDo), Encoding.UTF8, "application/json");

    var response = await _client.PostAsync($"api/ToDoCrud/CreateUserToDos", jsonContent);

    Assert.Equal(204, ((int)response.StatusCode));

  }
}

public class TestToken{ public string token { get; set; } = string.Empty; }
