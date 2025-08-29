import { Component, OnInit } from "@angular/core";
import { ToastService } from "../../services";

@Component({
  selector: "nc-toast",
  templateUrl: "./toast.component.html",
  styleUrls: ["./toast.component.scss"],
  standalone: false
})
export class ToastComponent implements OnInit {
  toastList: string[] = [];

  constructor(private toastService: ToastService) { }

  ngOnInit(): void {
    this.toastService.toastEvent.subscribe((toast) => {
      this.toastList.push(toast.message);
      setTimeout(() => {
        this.toastList.pop();
      }, 3000);
    });
  }
}
