namespace ProjectRider.App.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class ProjectController : Controller
    {
        private readonly ProjectRiderDbContext context;

        public ProjectController(ProjectRiderDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            var currentDbToShow = context.Projects.ToList();
            return View(currentDbToShow);
        }


        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            context.Projects.Add(project);
            context.SaveChanges();

            return RedirectToAction("Index"); ;

        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index"); ;
            }

            var currentProject = this.context.Projects.FirstOrDefault(x => x.Id == id);

            return View(currentProject);
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(int id, Project projectModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var projectToEdit = this.context.Projects.FirstOrDefault(x => x.Id == id);
            projectToEdit.Title = projectModel.Title;
            projectToEdit.Description = projectModel.Description;
            projectToEdit.Budget = projectModel.Budget;
            this.context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var currentProject = this.context.Projects.FirstOrDefault(x => x.Id == id);

            return View(currentProject);
        }

        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id, Project projectModel)
        {
            if (id==null)
            {
                return RedirectToAction("Index");
            }

            var projectToDelete = this.context.Projects.Find(id);
            this.context.Projects.Remove(projectToDelete);
            this.context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}