import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'nc-index',
  standalone: false,
  templateUrl: './index.component.html',
  styleUrl: './index.component.scss'
})
export class IndexComponent {

  constructor(
    private router: Router,
  ) { }

  logout() {
    this.router.navigate(["login"]);
  }
}
