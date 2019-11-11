const routes = [
  {
    path: "/",
    component: () => import("layouts/LandingLayout.vue"),
    children: [
      { path: "", component: () => import("pages/landing-page/Index.vue") }
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
      {
        path: "courses",
        component: () => import("pages/main/courses/CourseList.vue")
      },
      {
        path: "lessons",
        component: () => import("pages/main/lessons/LessonList.vue")
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
