import { isDevMode, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { IndexComponent } from './index/index.component';
import { ControlsModule } from '../controls/controls.module';

const routes: Routes = [
  { path: "login", component: LoginComponent },
  {
    path: "",
    component: IndexComponent,
    children: [
      { path: "admin", loadChildren: () => import("./admin/admin.module").then((m) => m.AdminModule) },
    ],
  },
  { path: "**", redirectTo: "login" },
];


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    IndexComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ControlsModule,
    RouterModule.forRoot(routes, {
      preloadingStrategy: PreloadAllModules,
      enableTracing: isDevMode(),
    }),],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
