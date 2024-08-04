import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { TaskService } from './app/Services/task.service';
import { LoginService } from './app/Services/login.service';
import { AppComponent } from './app/app.component';
import { ActivatedRoute, RouterModule, Routes, provideRouter } from '@angular/router';
import { LoginComponent } from './app/Components/Login/login.component';
import { UpsertTaskComponent } from './app/Components/Task/Upsert/upsert-task.component';
import { TaskListComponent } from './app/Components/Task/task-list.component';
import { UserService } from './app/Services/user.service';
import { UserUpsertComponent } from './app/Components/User/user-upsert.component';


//platformBrowserDynamic().bootstrapModule(AppModule)
//  .catch(err => console.error(err));

const routes: Routes = [
  { path: '', redirectTo: "/login/0", pathMatch: "full" },
  { path: 'login/:responseCode', component: LoginComponent },
  { path: 'upsert-task/:TaskId/:closedTaskStatusId', component: UpsertTaskComponent },
  { path: 'taskList', component: TaskListComponent },
  { path: 'upsertUser/:isUpdate', component: UserUpsertComponent }
];

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(withFetch()),
    TaskService,
    LoginService,
    UserService,
    { provide: ActivatedRoute, useValue: '' },
    provideRouter(routes)
  ]
})
  .catch((err) => console.error(err));
