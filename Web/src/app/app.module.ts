import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { isDevMode, NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

import { ControlsModule } from '../controls/controls.module';
import { AuthenticationGuard, AuthorizationGuard } from '../guards';
import { CreateAccountComponent } from './identity/create-account/create-account.component';
import { LoginComponent } from './login/login.component';
import { IndexComponent } from './index/index.component';

const routes: Routes = [
  // { path: "login", component: LoginComponent },
  { path: "identity/create-account", component: CreateAccountComponent },
  { path: "identity", loadChildren: () => import("./identity/identity.module").then((m) => m.IdentityModule) },
  {
    path: "",
    component: IndexComponent,
    canActivate: [AuthenticationGuard],
    canActivateChild: [AuthorizationGuard],
    children: [
      { path: "admin", loadChildren: () => import("./admin/admin.module").then((m) => m.AdminModule) },
    ],
  },
  { path: "**", redirectTo: "identity/create-account" },
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
    BrowserAnimationsModule,
    RouterModule.forRoot(routes, {
      preloadingStrategy: PreloadAllModules,
      enableTracing: isDevMode(),
    }),],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
