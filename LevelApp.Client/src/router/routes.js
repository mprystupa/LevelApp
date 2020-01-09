const routes = [
  {
    path: "/",
    component: () => import("layouts/LandingLayout.vue"),
    children: [
      {
        path: "",
        component: () => import("pages/landing-page/Index.vue")
      }
    ]
  },
  {
    path: "/main",
    component: () => import("layouts/MainLayout.vue"),
    children: [
      {
        path: "",
        component: () => import("pages/main/dashboard/Dashboard.vue")
      },
      // Courses
      {
        path: "courses",
        component: () => import("pages/main/courses/CourseList.vue")
      },
      {
        path: "courses/add",
        component: () => import("pages/main/courses/edit-course/EditCourse.vue")
      },
      {
        path: "courses/edit/:id",
        component: () => import("pages/main/courses/edit-course/EditCourse.vue")
      },
      {
        path: "courses/view/:id",
        component: () => import("pages/main/courses/view-course/ViewCourse.vue")
      },
      // Lessons
      {
        path: "lessons",
        component: () => import("pages/main/lessons/LessonList.vue")
      },
      {
        path: "lessons/add",
        component: () => import("pages/main/lessons/edit-lesson/EditLesson.vue")
      },
      {
        path: "lessons/edit/:id",
        component: () => import("pages/main/lessons/edit-lesson/EditLesson.vue")
      },
      {
        path: "lessons/view/:id",
        component: () => import("pages/main/lessons/view-lesson/ViewLesson.vue")
      }
    ]
  }
];

// Always leave this as last one
if (process.env.MODE !== "ssr") {
  routes.push({
    path: "*",
    component: () => import("pages/Error404.vue")
  });
}

export default routes;
