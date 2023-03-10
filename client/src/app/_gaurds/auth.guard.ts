import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlSerializer, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService:AccountService,private toaster:ToastrService){

  }
  canActivate():
    Observable<boolean> {
      return this.accountService.currentUser$.pipe(
    map(User=>{
      if(User) return true;
      else
      {
      this.toaster.error("you shall not pass");
      return false;
      }
    })
      )
  }
  
}
