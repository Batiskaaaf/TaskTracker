using Xunit;
using FakeItEasy;
using TaskTracker.Api.Controllers;
using AutoMapper;
using TaskTracker.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Model;
using TaskTracker.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskTracker.Model.DTO;
using TaskTracker.Data.Repository.IRepository;
using FluentAssertions;

namespace TaskTracker.Tests.Controller
{
    public class ProjectsControllerTests
    {

        private readonly IMapper mapper;
        private readonly IProjectRepository repo;
        public ProjectsControllerTests()
        {
            repo = A.Fake<IProjectRepository>();
            mapper = A.Fake<IMapper>();
        }          

        [Fact]
        public async void ProjectsController_Get_ReturnsOk()
        {
            //Arrange
            var projects = A.Fake<IEnumerator<Project>>();
            var projectsDTOList = A.Fake<List<ProjectDTO>>();
            A.CallTo(() => mapper.Map<List<ProjectDTO>>(projects)).Returns(projectsDTOList);
            var controller = new ProjectsController(repo, mapper);

            //Act
            var result = await controller.Get();

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }



        [Fact]
        public async void ProjectsController_GetId_ReturnOk()
        {
            //Arrane
            int id = 1;
            var project = A.Fake<Project>();
            var projectDTO = A.Fake<ProjectDTO>();
            A.CallTo(() => repo.GetById(id)).Returns(project);
            A.CallTo(() => mapper.Map<ProjectDTO>(project)).Returns(projectDTO);
            var controller = new ProjectsController(repo,mapper);

            //Act
            var result = await controller.Get(id);

            //Assert
            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void ProjectsController_GetId_ReturnNotFound()
        {
            //Arrane
            int id = 1;
            A.CallTo(() => repo.GetById(id)).Returns(null);
            var controller = new ProjectsController(repo, mapper);

            //Act
            var result = await controller.Get(id);

            //Assert
            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(NotFoundResult));
        }



        [Fact]
        public async void ProjectsController_Create_ReturnsOk()
        {
            //Arrange
            var projectDto = A.Fake<ProjectDTO>();
            var project = A.Fake<Project>();
            A.CallTo(() => mapper.Map<Project>(projectDto)).Returns(project);
            var controller = new ProjectsController(repo, mapper);


            //Act
            var result = await controller.Create(projectDto);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));

        }
        [Fact]
        public async void ProjectsController_Create_ReturnsBadRequest()
        {
            //Arrange
            var controller = new ProjectsController(repo, mapper);


            //Act
            var result = await controller.Create(null);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }



        [Fact]
        public async void ProjectsController_Delete_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var project = A.Fake<Project>();
            A.CallTo(() => repo.GetById(id)).Returns(project);
            var controller = new ProjectsController(repo, mapper);

            //Act
            var result = await controller.Delete(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void ProjectsController_Delete_ReturnsNotFound()
        {
            //Arrange
            int id = 0;
            A.CallTo(() => repo.GetById(id)).Returns(null);
            var controller = new ProjectsController(repo, mapper);

            //Act
            var result = await controller.Delete(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NotFoundResult));
        }



        [Fact]
        public async void ProjectsController_Edit_ReturnsOk()
        {
            int id = 1;
            var projectDto = A.Fake<ProjectDTO>();
            projectDto.Id = id;
            var project = A.Fake<Project>();
            A.CallTo(() => repo.GetById(id)).Returns(project);
            var controller = new ProjectsController(repo,mapper);

            var result = await controller.Edit(id, projectDto);

            result.Result.Should().NotBeNull();
        }
    }
}