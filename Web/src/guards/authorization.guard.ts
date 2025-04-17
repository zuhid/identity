import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateChild, GuardResult, MaybeAsync, RouterStateSnapshot } from "@angular/router";
import { TokenService } from "../services";

@Injectable({ providedIn: "root" })
export class AuthorizationGuard implements CanActivateChild {
  constructor(private tokenService: TokenService) { }

  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    // const role = (childRoute.data as any).policy;
    // if (role == "admin") return false;
    return true;
  }
}
