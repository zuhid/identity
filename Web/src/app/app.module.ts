import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { inject, isDevMode, NgModule, provideAppInitializer } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

import { ControlsModule } from '../controls/controls.module';
import { AuthenticationGuard, AuthorizationGuard } from '../guards';
import { IndexComponent } from './index/index.component';
import { ConfigService } from '@src/services';

const routes: Routes = [
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
  { path: "**", redirectTo: "identity/register" },
];

// Get the application configuration before the app initializes
const initializeAppFn = () => inject(ConfigService).loadConfig();

@NgModule({
  declarations: [
    AppComponent,
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
  providers: [provideAppInitializer(initializeAppFn)],
  bootstrap: [AppComponent]
})
export class AppModule { }
