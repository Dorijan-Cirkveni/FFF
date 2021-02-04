import React from "react";
import { Redirect } from "react-router-dom";

import StudentLessons from '../student/Lesson/StudentLessons';
import TeacherLessons from '../teacher/Lesson/TeacherLessons';


function Lessons(props) {
     switch (props.user.role) {

        case "Student":
            return < StudentLessons user={props.user} accessToken={props.accessToken} />;
        case "Teacher":
             return <TeacherLessons user={props.user} accessToken={props.accessToken} />;
        default:
            return <Redirect to="/" />;
    }
    }

export default Lessons;