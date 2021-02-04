import React from "react";
import { Redirect } from "react-router-dom";

import PageWrapper from "./PageWrapper";
import ContentWraper from "./ContentWraper";

import StudentCalendar from '../student/Calendar/StudentCalendar';
import CalendarWrapper from '../teacher/Calendar/TeacherCalendar';
import AdminCalendar from '../admin/Calendar/AdminCalendar';


function Calendar(props) {
    switch (props.user.role) {
        case "Student":
            return (
                <PageWrapper>
                    <ContentWraper>
                        < StudentCalendar user={props.user} accessToken={props.accessToken} />
                    </ContentWraper>
                </PageWrapper>);
        case "Teacher":
            return (

        <PageWrapper>
                            <ContentWraper>
                        <CalendarWrapper user={props.user} accessToken={props.accessToken} />
                    </ContentWraper>
                </PageWrapper>);
        case "Admin":
            return (

        <PageWrapper>
                                    <ContentWraper>
                        <AdminCalendar user={props.user} accessToken={props.accessToken} />
                    </ContentWraper>
                </PageWrapper>);
        default:
            return <Redirect to="/" />;
    }
}

export default Calendar;