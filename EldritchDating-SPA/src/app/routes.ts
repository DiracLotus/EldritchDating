import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberListComponent } from './member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';

export const appRoutes: Routes = [
    // Ordering is important - first wins matching
    { path: 'home', component: HomeComponent },
    { path: 'lists', component: ListsComponent },
    { path: 'members', component: MemberListComponent },
    { path: 'messages', component: MessagesComponent },
    { path: '**', redirectTo: 'home', pathMatch: 'full' }
];
