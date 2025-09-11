import { Component, OnInit } from "@angular/core";
import { ToastService } from "../../services";
import { Toast } from "@src/models";

@Component({
  selector: "nc-toast",
  templateUrl: "./toast.component.html",
  styleUrls: ["./toast.component.scss"],
  standalone: false
})
export class ToastComponent implements OnInit {
  toastList: Toast[] = [];

  constructor(private toastService: ToastService) { }

  ngOnInit(): void {
    this.toastService.toastEvent.subscribe((toast) => {
      this.toastList.push(toast);
      setTimeout(() => {
        this.toastList.pop();
      }, 3000);
    });
  }

  alertType(type: string): string {
    switch (type) {
      case 'error': return 'alert-danger';
      case 'info': return 'alert-info';
      case 'secondary': return 'alert-secondary';
      case 'success': return 'alert-success';
      case 'warning': return 'alert-warning';
      default: return 'alert-secondary';
    }
  }
}
