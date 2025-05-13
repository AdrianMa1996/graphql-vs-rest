import { Routes } from '@angular/router';

import { OverviewComponent } from './pages/overview/overview.component';
import { LoginComponent } from './pages/login/login.component';
import { SettingsComponent } from './pages/settings/settings.component';
import { EditProfileComponent } from './pages/edit-profile/edit-profile.component';
import { EditUserComponent } from './pages/edit-user/edit-user.component';
import { CreateUserComponent } from './pages/create-user/create-user.component';
import { EditProjectComponent } from './pages/edit-project/edit-project.component';
import { CreateProjectComponent } from './pages/create-project/create-project.component';
import { DiscussionsComponent } from './pages/discussions/discussions.component';
import { CreateDiscussionComponent } from './pages/create-discussion/create-discussion.component';
import { DiscussionComponent } from './pages/discussion/discussion.component';
import { IdeasComponent } from './pages/ideas/ideas.component';
import { IdeaComponent } from './pages/idea/idea.component';
import { CreateIdeaComponent } from './pages/create-idea/create-idea.component';
import { BugsComponent } from './pages/bugs/bugs.component';
import { BugComponent } from './pages/bug/bug.component';
import { CreateBugComponent } from './pages/create-bug/create-bug.component';

import { authGuard } from './guards/auth/auth.guard';
import { adminGuard } from './guards/admin/admin.guard';
import { canEditProjectGuard } from './guards/can-edit-project/can-edit-project.guard';

export const routes: Routes = [
    {path: '', component: OverviewComponent, canActivate: [authGuard]},
    {path: 'login', component: LoginComponent},
    {path: 'settings', component: SettingsComponent, canActivate: [adminGuard]},
    {path: 'edit-profile/:userId', component: EditProfileComponent},
    {path: 'edit-user/:userId', component: EditUserComponent},
    {path: 'create-user', component: CreateUserComponent},
    {path: 'edit-project/:projectId', component: EditProjectComponent, canActivate: [canEditProjectGuard]},
    {path: 'create-project', component: CreateProjectComponent},
    {path: 'discussions/:projectId', component: DiscussionsComponent},
    {path: 'discussion/:discussionId', component: DiscussionComponent},
    {path: 'create-discussion/:projectId', component: CreateDiscussionComponent},
    {path: 'ideas/:projectId', component: IdeasComponent},
    {path: 'idea/:ideaId', component: IdeaComponent},
    {path: 'create-idea/:projectId', component: CreateIdeaComponent},
    {path: 'bugs/:projectId', component: BugsComponent},
    {path: 'bug/:bugId', component: BugComponent},
    {path: 'create-bug/:projectId', component: CreateBugComponent},
];
