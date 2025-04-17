import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { TokenService } from '../services';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: "root" })
export class AuthenticationGuard implements CanActivate {
  constructor(public router: Router, private tokenService: TokenService) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    if (this.tokenService.isAuthenticated()) {
      return true;
    }
    return this.router.navigate(["login"]);
  }
}
