#Input dataset
ExpDegree <- read.csv("ExpectedDegree.csv", header=FALSE)
ActualDegreeADR <- read.csv("ActualDegreeADR.csv", header=FALSE)
ActualDegreeTRPW <- read.csv("ActualDegreeTRPW.csv", header=FALSE)
ExpTriangle <- read.csv("ExpectedTriangleDegree.csv", header=FALSE)
ActualTriangleADR <- read.csv("ActualTriangleDegreeADR.csv", header=FALSE)
ActualTriangleTRPW <- read.csv("ActualTriangleDegreeTRPW.csv", header=FALSE)
ExpCoefficient <- read.csv("ExpectedCoefficient.csv", header=FALSE)
ActualCoefficientADR <- read.csv("ActualCoefficientADR.csv", header=FALSE)
ActualCoefficientTRPW <- read.csv("ActualCoefficientTRPW.csv", header=FALSE)

#Rename
names(ExpDegree) <- c("ExpDegree")
names(ActualDegreeADR) <- c("ActualDegreeADR")
names(ActualDegreeTRPW) <- c("ActualDegreeTRPW")
names(ExpTriangle) <- c("ExpTriangle")
names(ActualTriangleADR) <- c("ActualTriangleADR")
names(ActualTriangleTRPW) <- c("ActualTriangleTRPW")
names(ExpCoefficient) <- c("ExpCoefficient")
names(ActualCoefficientADR) <- c("ActualCoefficientADR")
names(ActualCoefficientTRPW) <- c("ActualCoefficientTRPW")

#Round
ExpDegreeRound <- round(ExpDegree,0)
ExpTriangleRound <- round(ExpTriangle,0)
ExpCoefficientRound <- round(ExpCoefficient,1)
ActualCoefficientADRRound <- round(ActualCoefficientADR,1)
ActualCoefficientTRPWRound <- round(ActualCoefficientTRPW,1)

#Sort
ExpDegreeSorted <- data.frame(sort(ExpDegreeRound$ExpDegree))
ActualDegreeADRSorted <- data.frame(sort(ActualDegreeADR$ActualDegreeADR))
ActualDegreeTRPWSorted <- data.frame(sort(ActualDegreeTRPW$ActualDegreeTRPW))
ExpTriangleSorted <- data.frame(sort(ExpTriangleRound$ExpTriangle))
ActualTriangleADRSorted <- data.frame(sort(ActualTriangleADR$ActualTriangleADR))
ActualTriangleTRPWSorted <- data.frame(sort(ActualTriangleTRPW$ActualTriangleTRPW))
ExpCoefficientSorted <- data.frame(sort(ExpCoefficientRound$ExpCoefficient))
ActualCoefficientADRSorted <- data.frame(sort(ActualCoefficientADRRound$ActualCoefficientADR))
ActualCoefficientTRPWSorted <- data.frame(sort(ActualCoefficientTRPWRound$ActualCoefficientTRPW))

#Find times of exisits
ExpDegreeCount <- data.frame(table(ExpDegreeSorted$sort.ExpDegreeRound.ExpDegree.))
names(ExpDegreeCount) <- c("Degree", "Count")
ActualDegreeADRCount <- data.frame(table(ActualDegreeADRSorted$sort.ActualDegreeADR.ActualDegreeADR.))
names(ActualDegreeADRCount) <- c("Degree", "Count")
ActualDegreeTRPWCount <- data.frame(table(ActualDegreeTRPWSorted$sort.ActualDegreeTRPW.ActualDegreeTRPW.))
names(ActualDegreeTRPWCount) <- c("Degree", "Count")
ExpTriangleCount <- data.frame(table(ExpTriangleSorted$sort.ExpTriangleRound.ExpTriangle.))
names(ExpTriangleCount) <- c("TriangleDegree", "Count")
ActualTriangleADRCount <- data.frame(table(ActualTriangleADRSorted$sort.ActualTriangleADR.ActualTriangleADR.))
names(ActualTriangleADRCount) <- c("TriangleDegree", "Count")
ActualTriangleTRPWCount <- data.frame(table(ActualTriangleTRPWSorted$sort.ActualTriangleTRPW.ActualTriangleTRPW.))
names(ActualTriangleTRPWCount) <- c("TriangleDegree", "Count")
ExpCoefficientCount <- data.frame(table(ExpCoefficientSorted$sort.ExpCoefficientRound.ExpCoefficient.))
names(ExpCoefficientCount) <- c("Coefficient", "Count")
ActualCoefficientADRCount <- data.frame(table(ActualCoefficientADRSorted$sort.ActualCoefficientADRRound.ActualCoefficientADR.))
names(ActualCoefficientADRCount) <- c("Coefficient", "Count")
ActualCoefficientTRPWCount <- data.frame(table(ActualCoefficientTRPWSorted$sort.ActualCoefficientTRPWRound.ActualCoefficientTRPW.))
names(ActualCoefficientTRPWCount) <- c("Coefficient", "Count")



#Calculate Percentages
ExpDegreeFinal <- within(ExpDegreeCount, {
  percentage = Count / nrow(ExpDegreeSorted)
  group="Expected"
})
ActualDegreeADRFinal <- within(ActualDegreeADRCount, {
  percentage = Count / nrow(ActualDegreeADRSorted)
  group="ADR"
})
ActualDegreeTRPWFinal <- within(ActualDegreeTRPWCount, {
  percentage = Count / nrow(ActualDegreeTRPWSorted)
  group="TRPW"
})
ExpTriangleFinal <- within(ExpTriangleCount, {
  percentage = Count / nrow(ExpTriangleSorted)
  group="Expected"
})
ActualTriangleADRFinal <- within(ActualTriangleADRCount, {
  percentage = Count / nrow(ActualTriangleADRSorted)
  group="ADR"
})
ActualTriangleTRPWFinal <- within(ActualTriangleTRPWCount, {
  percentage = Count / nrow(ActualTriangleTRPWSorted)
  group="TRPW"
})
ExpCoefficientFinal <- within(ExpCoefficientCount, {
  percentage = Count / nrow(ExpCoefficientSorted)
  group="Expected"
})
ActualCoefficientADRFinal <- within(ActualCoefficientADRCount, {
  percentage = Count / nrow(ActualCoefficientADRSorted)
  group="ADR"
})
ActualCoefficientTRPWFinal <- within(ActualCoefficientTRPWCount, {
  percentage = Count / nrow(ActualCoefficientTRPWSorted)
  group="TRPW"
})


#Plot
library(ggplot2)
#Bind data
DegreeFinal <- rbind(ExpDegreeFinal, ActualDegreeADRFinal, ActualDegreeTRPWFinal)
TriangleFinal <- rbind(ExpTriangleFinal, ActualTriangleADRFinal, ActualTriangleTRPWFinal)
CoefficientFinal <- rbind(ExpCoefficientFinal, ActualCoefficientADRFinal, ActualCoefficientTRPWFinal)

#Vertex Degree
ggplot(DegreeFinal, aes(x=Degree,y=percentage,group=group,color=group,shape=group))+
  geom_point()+
  geom_line()+
  scale_y_log10(limits=c(1e-3,1))+
  # scale_y_log10()+
  coord_cartesian(xlim=c(0,20))+
  scale_x_discrete(breaks=seq(0,20,by=1))+
  # coord_cartesian(xlim=c(0,100))+
  # scale_x_discrete(breaks=seq(0,100,by=5))+
  labs(title="Vertex Degree Distribution Email", x="Vertex Degree", y="Percentage Vertices")
#Triangle Degree
ggplot(TriangleFinal, aes(x=TriangleDegree,y=percentage,group=group,color=group,shape=group))+
  geom_point()+
  geom_line()+
  scale_y_log10(limits=c(1e-3,1))+
  coord_cartesian(xlim=c(0,20))+
  scale_x_discrete(breaks=seq(0, 20, by=1))+
  labs(title="Triangle Degree Distribution Email", x="Triangle Degree", y="Percentage Vertices")
#Clustering Coefficient
ggplot(CoefficientFinal, aes(x=Coefficient,y=percentage,group=group,color=group,shape=group))+
  geom_point()+
  geom_line()+
  scale_y_log10()+
  coord_cartesian(xlim=c(0,11))+
  scale_x_discrete(breaks=seq(0, 1, by=0.1))+
  labs(title="Clustering Coefficient Distribution Email", x="Clustering Coefficient Degree", y="Percentage Vertices")

#Triangle count
ExpTriangleCount <- data.frame(sum(as.numeric(ExpTriangleFinal$TriangleDegree)*as.numeric(ExpTriangleFinal$Count)))
names(ExpTriangleCount) <- c("TriangleDegree")
ADRTriangleCount <- data.frame(sum(as.numeric(ActualTriangleADRFinal$TriangleDegree)*as.numeric(ActualTriangleADRFinal$Count)))
names(ADRTriangleCount) <- c("TriangleDegree")
TRPWTriangleCount <- data.frame(sum(as.numeric(ActualTriangleTRPWFinal$TriangleDegree)*as.numeric(ActualTriangleTRPWFinal$Count)))
names(TRPWTriangleCount) <- c("TriangleDegree")
ExpTriangleCountFinal <- within(ExpTriangleCount, {
  group = "Expected"
})
ADRTriangleCountFinal <- within(ADRTriangleCount, {
  group = "ADR"
})
TRPWTriangleCountFinal <- within(TRPWTriangleCount, {
  group = "TRPW"
})
TriangleCount <- rbind(ExpTriangleCountFinal, ADRTriangleCountFinal, TRPWTriangleCountFinal)


ggplot(TriangleCount, aes(x=group,y=TriangleDegree,group=group,fill=group))+
  geom_bar(stat="identity")+
  labs(title="Tiangle Count Email",x="Algorithm",y="Number of Triangles")+
  guides(fill=guide_legend(title="Algorithm"))


