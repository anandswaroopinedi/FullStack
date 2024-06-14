import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterOutlet } from '@angular/router';
import { LoginService } from './login.service';
import { AuthService } from '../Services/Auth/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule,RouterOutlet,CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm:FormGroup;
  dispLogin:boolean=true;
  constructor(private route:Router,private loginService:LoginService,private authService:AuthService)
  {
    this.loginForm=new FormGroup({
      name:new FormControl('',[Validators.required]),
      password: new FormControl('',[Validators.required])
    });
  }
  onSubmit()
  {
    console.log(this.loginForm.value);
    if(this.loginForm.valid)
    {
      this.dispLogin=false;
      this.loginService.postLoginDetails(this.loginForm.value).subscribe((value)=>{
        this.authService.setToken(value);
        this.route.navigate(["/home"]);
      })
    }
  }
}
