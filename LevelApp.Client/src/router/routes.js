const routes = [
  {
    path: "/",
    component: () => import("layouts/HomeLayout.vue"),
    children: [
      { path: "", component: () => import("pages/landing-page/Index.vue") },
      { path: "/login", component: () => import("pages/login-page/Login.vue") }
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
