
/****** Object:  Table [dbo].[EventTaskTimes]    Script Date: 12/3/2015 8:48:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EventTaskTimes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventDate] [datetime] NOT NULL,
	[EventTaskListId] [int] NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
 CONSTRAINT [PK_EventTaskTimes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EventTaskTimes]  WITH CHECK ADD  CONSTRAINT [FK_EventTaskTimes_EventTaskLists] FOREIGN KEY([EventTaskListId])
REFERENCES [dbo].[EventTaskLists] ([Id])
GO

ALTER TABLE [dbo].[EventTaskTimes] CHECK CONSTRAINT [FK_EventTaskTimes_EventTaskLists]
GO


