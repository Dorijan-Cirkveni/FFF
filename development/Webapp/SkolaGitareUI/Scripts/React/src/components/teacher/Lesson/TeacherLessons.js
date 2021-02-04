import React from "react";
import PageWrapper from "../../shared/PageWrapper";
import ContentWraper from "../../shared/ContentWraper";
import LessonTabs from './LessonTabs';


const LessonsPage = (props) => {
    return (
        <PageWrapper>
            <ContentWraper title={"LEKCIJE"}>
                <LessonTabs accessToken={props.accessToken}/>
            </ContentWraper>
        </PageWrapper>
                 
    )
};

export default LessonsPage;