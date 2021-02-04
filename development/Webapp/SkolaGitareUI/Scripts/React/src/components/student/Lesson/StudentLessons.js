import React from 'react';

import PageWrapper from "../../shared/PageWrapper";
import ContentWraper from "../../shared/ContentWraper";
import LessonTabs from './LessonList';


const StudentLessons = (props) => {
   
    return (
        <PageWrapper>
            <ContentWraper title={"LEKCIJE"}>
                <LessonTabs accessToken={props.accessToken} user={props.user} />
            </ContentWraper>
        </PageWrapper>
    );
}

export default StudentLessons;

