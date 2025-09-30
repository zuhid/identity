import { AuthenticationGuard, AuthorizationGuard } from '../guards';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { inject, isDevMode, NgModule, provideAppInitializer } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { ConfigService } from '@src/services';
import { ControlsModule } from '@src/controls/controls.module';

// Components
import { IndexComponent } from './index/index.component';
import { LoginComponent } from './identity/login/login.component';
import { RegisterComponent } from './identity/register/register.component';
import { TfaComponent } from './identity/tfa/tfa.component';

const routes: Routes = [
  { path: "identity", loadChildren: () => import("./identity/identity.module").then((m) => m.IdentityModule) },
  // {
  //   path: "",
  //   component: IndexComponent,
  //   canActivate: [AuthenticationGuard],
  //   canActivateChild: [AuthorizationGuard],
  //   children: [
  //     { path: "identity", loadChildren: () => import("./identity/identity.module").then((m) => m.IdentityModule) },
  //   ],
  // },
  { path: "**", redirectTo: "identity/register" },
];

// Get the application configuration before the app initializes
const initializeAppFn = () => inject(ConfigService).loadConfig();

@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    LoginComponent,
    RegisterComponent,
    TfaComponent,
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
  providers: [provideAppInitializer(initializeAppFn)],
  bootstrap: [AppComponent]
})
export class AppModule { }
