import React from "react";
import Tabs from "@material-ui/core/Tabs";
import Tab from "@material-ui/core/Tab";
import Paper from "@material-ui/core/Paper";
import AddIcon from "@material-ui/icons/Add";
import SearchIcon from "@material-ui/icons/Search";
import QueryBuilderIcon from "@material-ui/icons/QueryBuilder";
import TabPanel from "../../shared/TabPanel";
import NewLesson from "./NewLesson";
import LessonList from "./LessonList";



function a11yProps(index) {
    return {
        id: `lesson-tab-${index}`,
        "aria-controls": `lesson-tabpanel-${index}`,
    };
}



export default function LessonTabs(props) {
    const [value, setValue] = React.useState(0);

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    return (
        <div>
            <Paper square >
                  <Tabs
                       value={value}
                            onChange={handleChange}
                            indicatorColor="secondary"
                            textColor="secondary  "
                            aria-label="icon tabs example"
                            centered
                        >
                            <Tab
                                icon={<QueryBuilderIcon />}
                                aria-label="last"
                                {...a11yProps(0)}
                            />
                            <Tab
                                icon={<SearchIcon />}
                                aria-label="search"
                                {...a11yProps(1)}
                            />
                            <Tab icon={<AddIcon />} aria-label="add" {...a11yProps(0)} />
                        </Tabs>
                    </Paper>
            <TabPanel value={value} index={0}>
                <LessonList url="Lessons/NextAppointment" accessToken={props.accessToken} title="U budućem terminu"></LessonList>
                    </TabPanel>
                    <TabPanel value={value} index={1}>
                <LessonList url="Lessons" accessToken={props.accessToken} title="Sve lekcije"></LessonList>
                    </TabPanel>
                    <TabPanel value={value} index={2}>
                <NewLesson accessToken={props.accessToken}></NewLesson>
            </TabPanel>
            </div>
            
    );
}
